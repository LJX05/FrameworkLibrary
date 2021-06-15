using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using YxSoft.Core.AOP;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// 服务提供者
    /// </summary>
    public sealed class ServiceProvider : IDefaultServiceProvider,IServiceProvider
    {
        readonly IEnumerable<ServiceDescriptor> descriptors;
        readonly bool dynmicProxy;
        private MethodInfo getProxyMethod;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="dynmicProxy">是否动态代理</param>
        internal ServiceProvider(IEnumerable<ServiceDescriptor> serviceDescriptors, bool dynmicProxy=false) {
            descriptors = serviceDescriptors;
            this.dynmicProxy = dynmicProxy;
            getProxyMethod = this.GetType().GetMethod("getProxy", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        }
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType)
        {
            var descriptor = descriptors.FirstOrDefault(o => o.ServiceType == serviceType);
            if (descriptor == null)
            {
                //throw new NotImplementedException("未找到指定类型");
                return null;
            }
            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
                 
                return descriptor.ImplementationInstance;
            }
            return CreartDefeat(descriptor);
        }
        
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="serviceType">接口类型</param>
        /// <returns></returns>
        public object GetService(Type serviceType, string uniqueness)
        {
            var descriptor = descriptors.FirstOrDefault(o => o.ServiceType == serviceType && o.Uniqueness == uniqueness);
            if (descriptor == null)
            {
                //throw new NotImplementedException("未找到指定类型");
                return null;
            }
            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
               
                return descriptor.ImplementationInstance;
            }
           
            return CreartDefeat(descriptor);
        }
        /// <summary>
        /// 反注入
        /// </summary>
        /// <returns></returns>
        public IServiceCollection GetServiceCollection()
        {
            return (IServiceCollection)descriptors;
        }

        /// <summary>
        /// 创建一个对象
        /// </summary>
        /// <param name="impType">实例真实类型</param>
        /// <returns></returns>
        private object CreartDefeat(ServiceDescriptor descriptor)
        {
            object obj;
            var impType = descriptor.ImplementationType ?? descriptor.ServiceType;
            if (descriptor.ImplementationFactory != null)
            {
                //如果有实例工厂那就进行实例工厂调用
                obj = descriptor.ImplementationFactory.Invoke(this);
            }
            else
            {
                var constructors = impType.GetConstructors();
                var constructor = constructors[0];//获取默认的构造
                var ctrParm = constructor.GetParameters();
                if (ctrParm.Length == 0)
                {
                    //反射创建
                    obj = Activator.CreateInstance(impType);
                }
                else
                {
                    var vs = new List<object>();
                    foreach (var item in ctrParm)
                    {
                        vs.Add(GetService(item.ParameterType));
                    }
                    obj = constructor.Invoke(vs.ToArray());
                }
            }
            //进行属性注入
            var properties = impType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            var needProperties = properties.Where(o => o.GetFirstAttribute<AutoInjectionAttribute>() != null);
            foreach (var item in needProperties)
            {
                object vaule;
                var autoInjection = item.GetFirstAttribute<AutoInjectionAttribute>();
                if (!string.IsNullOrEmpty(autoInjection.uniqueness))
                {
                    vaule = GetService(item.PropertyType, autoInjection.uniqueness);
                }
                else
                {
                    vaule = GetService(item.PropertyType);
                }
                item.SetValue(obj, vaule);
            }
            //是否进行代理
            if (dynmicProxy && descriptor.ServiceType.IsInterface)
            {
                var method = getProxyMethod.MakeGenericMethod(descriptor.ServiceType);
                return method.Invoke(this, new object[] { obj });
            }
            return obj;
        }
        /// <summary>
        /// 代理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="realobj"></param>
        /// <returns></returns>
        private object getProxy<T>(T realobj)
        {
            var dynamicProxy = new DynamicProxy<T>(realobj,this);
            return dynamicProxy.GetTransparentProxy();
        }
    }
}
