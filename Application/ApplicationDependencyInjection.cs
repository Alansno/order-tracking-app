using Application.User;
using Application.User.Commands;
using Application.User.Mappers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddUserUseCases().AddUserMappers();
        private static IServiceCollection AddUserUseCases(this IServiceCollection services)
            => services.AddScoped<UserUseCases>()
            .AddScoped<AddUser>();

        private static IServiceCollection AddUserMappers(this IServiceCollection services)
            => services.AddScoped<UserMapper>();
    }
}
