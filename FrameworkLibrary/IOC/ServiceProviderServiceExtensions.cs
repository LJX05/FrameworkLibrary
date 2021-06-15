using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    public static class ServiceProviderServiceExtensions
    {
        public static T GetService<T>(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return (T)provider.GetService(typeof(T));
        }

        public static T GetService<T>(this IServiceProvider provider, string Name)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (provider is IDefaultServiceProvider)
            {
                return (T)((IDefaultServiceProvider)provider).GetService(typeof(T), Name);
            }
            return provider.GetService<T>();
        }

        public static IServiceCollection GetServiceCollection(this IServiceProvider provider)
        {
            if (provider == null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            if (provider is IDefaultServiceProvider)
            {
                return ((IDefaultServiceProvider)provider).GetServiceCollection();
            }
            else {
                throw new ArgumentNullException(nameof(provider));
            }
        }
    }
}
