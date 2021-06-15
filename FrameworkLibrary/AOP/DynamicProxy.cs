using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;

namespace YxSoft.Core.AOP
{
    /// <summary>
    /// AOP自定义注解拦截代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicProxy<T> : RealProxy
    {
        private readonly T _decorated;

        private readonly IServiceProvider _serviceProvider;
        public DynamicProxy(T decorated)
          : base(typeof(T))
        {
            _decorated = decorated;
        }
        public DynamicProxy(T decorated, IServiceProvider serviceProvider)
         : base(typeof(T))
        {
            _decorated = decorated;
            this._serviceProvider = serviceProvider;
        }
        private void Log(string msg, object arg = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg, arg);
            Console.ResetColor();
        }
        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            //真实类型
            var _realType = _decorated.GetType();

            var parameters= methodInfo.GetParameters();
            var ptypes=parameters.Select(p => p.ParameterType).ToArray();
            //真实方法(接口的方法)
            var _readmethod = _realType.GetMethod(methodInfo.Name, ptypes);

            //获取所有的AOP特性
            var aops = _readmethod != null ? _readmethod.GetCustomAttributes(typeof(AspectAttribute), true).Cast<AspectAttribute>().OrderByDescending(o => o.ExecutionPriority).ToList() : new List<AspectAttribute>();
            foreach (var item in aops)
            {
                item.ImportService(_serviceProvider);
                item.OnBeforeExecute(_decorated, _realType, methodCall);
            }

            try
            {
                var result = methodInfo.Invoke(_decorated, methodCall.InArgs);
                foreach (var item in aops)
                {
                    item.OnAfterExecute();
                }
                return new ReturnMessage(result, null, 0,
                  methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                //异常了怎么处理
                foreach (var item in aops)
                {
                    item.OnErrorExecuting();
                }
                return new ReturnMessage(e, methodCall);
            }
        }
    }
}
