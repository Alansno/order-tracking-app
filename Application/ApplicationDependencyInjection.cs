using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DeliveryMan;
using Application.DeliveryMan.Commands;
using Application.DeliveryMan.Mappers;
using Application.Package;
using Application.Package.Commands;
using Application.Package.Mappers;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddDeliveryManUsesCases().AddDeliveryManMappers().AddPackageUseCases().AddPackageMappers();
        
        private static IServiceCollection AddDeliveryManUsesCases(this IServiceCollection services)
            => services.AddScoped<DeliveryManUseCases>()
                .AddScoped<AddDeliveryMan>();
        
        private static IServiceCollection AddPackageUseCases(this IServiceCollection services)
            => services.AddScoped<PackageUseCases>()
                .AddScoped<AddPackage>();

        private static IServiceCollection AddDeliveryManMappers(this IServiceCollection services)
            => services.AddScoped<DeliveryManMapper>();

        private static IServiceCollection AddPackageMappers(this IServiceCollection services)
            => services.AddScoped<PackageMapper>();

    }
}
