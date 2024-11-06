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
            => services.AddDeliveryManRepository().AddPackageRepository();

        private static IServiceCollection AddDeliveryManRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<DeliveryManEntity>, DeliveryManRepository>();
        
        private static IServiceCollection AddPackageRepository(this IServiceCollection services)
            => services.AddScoped<IRepository<PackageEntity>, PackageRepository>();

    }
}
