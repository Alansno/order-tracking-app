using Domain.Entities;
using Infrastructure.Repositories;
using Infrastructure.Repositories.IRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddDeliveryManRepository().AddPackageRepository()
                .AddCityRepository().AddProductRepository()
                .AddShippingRepository();

        private static IServiceCollection AddDeliveryManRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<DeliveryManEntity>, DeliveryManRepository>()
                .AddScoped<ISearchRepository<DeliveryManEntity>, DeliveryManRepository>()
                .AddScoped<IDeliveryManRepository, DeliveryManRepository>();
        
        private static IServiceCollection AddPackageRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<PackageEntity>, PackageRepository>()
                .AddScoped<IPackageRepository, PackageRepository>()
                .AddScoped<ISearchRepository<PackageEntity>, PackageRepository>();
        
        private static IServiceCollection AddCityRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<CityEntity>, CityRepository>();
        
        private static IServiceCollection AddProductRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<ProductEntity>, ProductRepository>()
                .AddScoped<IProductRepository, ProductRepository>();

        private static IServiceCollection AddShippingRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<ShippingEntity>, ShippingRepository>()
                .AddScoped<IShippingRepository, ShippingRepository>()
                .AddScoped<ISearchRepository<ShippingEntity>, ShippingRepository>();
    }
}
