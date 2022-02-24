using Microsoft.Extensions.DependencyInjection;
using ShopInterfaces;

namespace ShopImplementation.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection container)
        {
            container.AddScoped<IShopService, ShopService>();
            container.AddScoped<IArticleRepository, ArticleReposiotry>();
            container.AddScoped<ISupplierService, SupplierService>();
            container.AddScoped<ISupplierRepository, SupplierRepository>();
            return container;
        }
    }
}
