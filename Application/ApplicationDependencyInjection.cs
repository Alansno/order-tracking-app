using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.City;
using Application.City.Commands;
using Application.City.Mappers;
using Application.DeliveryMan;
using Application.DeliveryMan.Commands;
using Application.DeliveryMan.Mappers;
using Application.Package;
using Application.Package.Commands;
using Application.Package.Mappers;
using Application.Product;
using Application.Product.Commands;
using Application.Product.Mappers;
using Application.Shipping;
using Application.Shipping.Commands;
using Application.Shipping.Mappers;
using Application.Shipping.Services;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddDeliveryManUsesCases().AddDeliveryManMappers()
                .AddPackageUseCases().AddPackageMappers()
                .AddCityUseCases().AddCityMappers()
                .AddProductUseCases().AddProductMappers()
                .AddShippingUseCases().AddShippingMappers();
        
        private static IServiceCollection AddDeliveryManUsesCases(this IServiceCollection services)
            => services.AddScoped<DeliveryManUseCases>()
                .AddScoped<AddDeliveryMan>()
                .AddScoped<GetDeliveryManWith>();
        
        private static IServiceCollection AddPackageUseCases(this IServiceCollection services)
            => services.AddScoped<PackageUseCases>()
                .AddScoped<AddPackage>()
                .AddScoped<GetPackages>();
        
        private static IServiceCollection AddCityUseCases(this IServiceCollection services)
            => services.AddScoped<CityUseCases>()
                .AddScoped<AddCity>()
                .AddScoped<GetCities>()
                .AddScoped<GetCity>();
        
        private static IServiceCollection AddProductUseCases(this IServiceCollection services)
            => services.AddScoped<ProductUseCases>()
                .AddScoped<AddProduct>()
                .AddScoped<GetProducts>()
                .AddScoped<AddPackageInProduct>()
                .AddScoped<GetProduct>();
        
        private static IServiceCollection AddShippingUseCases(this IServiceCollection services)
            => services.AddScoped<ShippingUseCases>()
                .AddScoped<AddShipping>()
                .AddScoped<AssignShipmentService>()
                .AddScoped<ShipmentDelivered>();

        private static IServiceCollection AddDeliveryManMappers(this IServiceCollection services)
            => services.AddScoped<DeliveryManMapper>();

        private static IServiceCollection AddPackageMappers(this IServiceCollection services)
            => services.AddScoped<PackageMapper>();
        
        private static IServiceCollection AddCityMappers(this IServiceCollection services)
            => services.AddScoped<CityMapper>();
        
        private static IServiceCollection AddProductMappers(this IServiceCollection services)
            => services.AddScoped<ProductMapper>();

        private static IServiceCollection AddShippingMappers(this IServiceCollection services)
            => services.AddScoped<ShippingMapper>();
    }
}
