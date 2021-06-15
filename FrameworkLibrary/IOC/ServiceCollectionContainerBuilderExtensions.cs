using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YxSoft.Core.IOC
{
    /// <summary>
    /// Extension methods for building a <see cref="ServiceProvider"/> from an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {

        public static IServiceProvider BuildServiceProvider(this IServiceCollection services, bool proxy)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            return new ServiceProvider(services, proxy);
        }
    }
}
