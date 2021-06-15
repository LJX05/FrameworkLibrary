using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace YxSoft.Core.AOP
{
    /// <summary>
    /// 自定义注解
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AspectAttribute : Attribute
    {
       
        /// <summary>
        /// 执行优先度
        /// </summary>
        public int ExecutionPriority {
            get {
               return _executionPriority;
            } 
        }
        protected int _executionPriority;
        /// <summary>
        /// 真实调用方法
        /// </summary>
        protected IMethodCallMessage _methodCall;
        /// <summary>
        /// 真实对象
        /// </summary>
        protected object _readObject;
        /// <summary>
        /// 真实类型
        /// </summary>
        protected Type _readType;
        /// <summary>
        /// 服务提供商
        /// </summary>
        protected IServiceProvider serviceProvider { get; private set; }
        /// <summary>
        /// 服务注入
        /// </summary>
        /// <param name="_serviceProvider"></param>

        public void ImportService(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        public virtual void OnBeforeExecute(object readObject, Type readType, IMethodCallMessage methodCall)
        {
            this._methodCall = methodCall;
            this._readObject = readObject;
            this._readType = readType;
            _executionPriority = 0;
        }
        public virtual void OnAfterExecute()
        {
        }
        public virtual void OnErrorExecuting()
        {
        }

    }
}
