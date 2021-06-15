using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// 服务描述类
    /// </summary>
    public class ServiceDescriptor
    {
        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        /// <summary>
        /// 服务唯一性描述
        /// </summary>
        public string Uniqueness  { get { return GetUniqueness(); }
    }

        /// <summary>
        /// 服务的类型
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// 实例的类型
        /// </summary>
        public Type ImplementationType { get; }

        /// <summary>
        /// 实例对象
        /// </summary>
        public object ImplementationInstance { get; }
        /// <summary>
        /// 示例工厂
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; }
        /// <summary>
        /// 获取唯一型名称
        /// </summary>
        /// <returns></returns>
        private string GetUniqueness() {
            if (ImplementationType != null)
            {
                return ImplementationType.Name;
            }
            if (ImplementationInstance != null)
            {
                return ImplementationInstance.GetType().Name;
            }
            if (ImplementationFactory != null)
            {
                var type = ImplementationFactory.GetType().GetGenericArguments()[1];
                return type.Name;
            }
            return ServiceType.Name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetime"></param>
        private ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        public ServiceDescriptor(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime)
            : this(serviceType, lifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            ImplementationType = implementationType;
            
        }


        public ServiceDescriptor(Type serviceType,
            object instance)
            : this(serviceType, ServiceLifetime.Singleton)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
           
            ImplementationInstance = instance;
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="factory"></param>
        /// <param name="lifetime"></param>
        public ServiceDescriptor(
            Type serviceType,
            Func<IServiceProvider, object> factory,
            ServiceLifetime lifetime)
            : this(serviceType, lifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            ImplementationFactory = factory;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementation"></param>
        /// <param name="factory"></param>
        /// <param name="lifetime"></param>
        public ServiceDescriptor(Type serviceType, Type implementation, Func<IServiceProvider, object> factory, ServiceLifetime lifetime) : this(serviceType, lifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            ImplementationFactory = factory;
            ImplementationType = implementation;
        }





        /// <summary>
        /// 获取实际类型
        /// </summary>
        /// <returns></returns>
        internal Type GetImplementationType()
        {
            if (ImplementationType != null)
            {
                return ImplementationType;
            }
            else if (ImplementationInstance != null)
            {
                return ImplementationInstance.GetType();
            }
            else if (ImplementationFactory != null)
            {
                var typeArguments = ImplementationFactory.GetType().GetGenericArguments();

                Debug.Assert(typeArguments.Length == 2);

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType, ImplementationInstance or ImplementationFactory must be non null");
            return null;
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Transient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Transient(Type service, Type implementationType)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Transient<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Transient<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Transient"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Transient(Type service, Func<IServiceProvider, object> implementationFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Transient);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Scoped<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/> and <paramref name="implementationType"/>
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationType">The type of the implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Scoped(Type service, Type implementationType)
        {
            return Describe(service, implementationType, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Scoped<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <paramref name="service"/>, <paramref name="implementationFactory"/>,
        /// and the <see cref="ServiceLifetime.Scoped"/> lifetime.
        /// </summary>
        /// <param name="service">The type of the service.</param>
        /// <param name="implementationFactory">A factory to create new instances of the service implementation.</param>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Scoped(Type service, Func<IServiceProvider, object> implementationFactory)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Scoped);
        }

        /// <summary>
        /// Creates an instance of <see cref="ServiceDescriptor"/> with the specified
        /// <typeparamref name="TService"/>, <typeparamref name="TImplementation"/>,
        /// and the <see cref="ServiceLifetime.Singleton"/> lifetime.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <returns>A new instance of <see cref="ServiceDescriptor"/>.</returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Singleton);
        }

        /// <summary>
        /// 普通型单例
        /// </summary>
        /// <param name="service"></param>
        /// <param name="implementationType"></param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton(Type service, Type implementationType)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="implementationFactory"></param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
        }

          /// <summary>
          /// 单例工厂型
          /// </summary>
          /// <typeparam name="TService"></typeparam>
          /// <param name="implementationFactory"></param>
          /// <returns></returns>
        public static ServiceDescriptor Singleton<TService>(Func<IServiceProvider, TService> implementationFactory)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton);
        }

       /// <summary>
       /// 单例工厂型
       /// </summary>
       /// <param name="serviceType"></param>
       /// <param name="implementationFactory"></param>
       /// <returns></returns>
        public static ServiceDescriptor Singleton(
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(serviceType, implementationFactory, ServiceLifetime.Singleton);
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="implementationInstance"></param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton<TService>(TService implementationInstance)
            where TService : class
        {
            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return Singleton(typeof(TService), implementationInstance);
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationInstance"></param>
        /// <returns></returns>
        public static ServiceDescriptor Singleton(
            Type serviceType,
            object implementationInstance)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return new ServiceDescriptor(serviceType, implementationInstance);
        }

        private static ServiceDescriptor Describe<TService, TImplementation>(ServiceLifetime lifetime)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(
                typeof(TService),
                typeof(TImplementation),
                lifetime: lifetime);
        }

        /// <summary>
        /// 普通描述
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static ServiceDescriptor Describe(Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            return new ServiceDescriptor(serviceType, implementationType, lifetime);
        }

        /// <summary>
        /// 普通描述
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationType"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static ServiceDescriptor Describe(Type serviceType, Type implementationType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            return new ServiceDescriptor(serviceType, implementationType, implementationFactory, lifetime);
        }

        /// <summary>
        /// 实例工厂描述
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="implementationFactory"></param>
        /// <param name="lifetime"></param>
        /// <returns></returns>
        public static ServiceDescriptor Describe(Type serviceType, Func<IServiceProvider, object> implementationFactory, ServiceLifetime lifetime)
        {
            return new ServiceDescriptor(serviceType, implementationFactory, lifetime);
        }
    }
}
