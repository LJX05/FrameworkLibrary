using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// Specifies the lifetime of a service in an <see cref="IServiceCollection"/>.
    /// </summary>
    public enum ServiceLifetime
    {
        /// <summary>
        /// 指定将创建该服务的单个实例
        /// Specifies that a single instance of the service will be created.
        /// </summary>
        Singleton = 0,
        /// <summary>
        /// 指定将为每个作用域创建服务的新实例。 在 ASP.NET Core 应用中，会针对每个服务器请求创建一个作用域
        /// Specifies that a new instance of the service will be created for each scope.
        /// In ASP.NET Core applications a scope is created around each server request.
        /// </summary>
        Scoped = 1,
        /// <summary>
        /// 指定每次请求服务时，将创建该服务的新实例。
        /// Specifies that a new instance of the service will be created every time it is requested.
        /// </summary>
        Transient = 2
    }
}
