using Logger;
using Microsoft.Extensions.DependencyInjection;
using ShopImplementation;
using ShopInterfaces;
using System;

namespace TheShop
{
    public static class ContainerBuilder
    {
        public static IServiceProvider Build()
        {
            ServiceCollection container = new ServiceCollection();
            container.AddScoped<IDatabaseDriver, DatabaseDriver>();
            container.AddScoped<IShopService, ShopService>();
            container.AddScoped<ILogger, Logger.Logger>();

            return container.BuildServiceProvider();
        }

        public static T GetInstance<T>(this IServiceProvider services)
        {
            return services.GetService<T>();
        }
    }
}
