using Util.Logger;
using Microsoft.Extensions.DependencyInjection;
using ShopImplementation;
using ShopInterfaces;
using System;
using Util.TimeProvider;

namespace TheShop
{
    public static class ContainerBuilder
    {
        public static IServiceProvider Build()
        {
            ServiceCollection container = new ServiceCollection();
            container.AddScoped<IArticleRepository, ArticleReposiotry>();
            container.AddScoped<IShopService, ShopService>();
            container.AddScoped<ILogger, Logger>();
            container.AddScoped<ILogType, ConsoleLogger>();

            container.AddScoped<ITimeProvider, TimeProvider>();

            return container.BuildServiceProvider();
        }

        public static T GetInstance<T>(this IServiceProvider services)
        {
            return services.GetService<T>();
        }
    }
}
