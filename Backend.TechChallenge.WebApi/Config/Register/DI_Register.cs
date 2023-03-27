using Backend.TechChallenge.Api.Base;
using Backend.TechChallenge.Application.Base;
using Backend.TechChallenge.Application.CustomServices;
using Backend.TechChallenge.Application.Interfaces.Base;
using Backend.TechChallenge.Application.Interfaces.CustomServices;
using Backend.TechChallenge.Infrastructure.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Base;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Api.Config.Register
{
    public static class DI_Register
    {
        public static IServiceCollection InitRegistration(this IServiceCollection services)
        {
            InitCommonRegistration(services);

            InitCustomRepositoryRegistration(services);
            InitCustomServiceRegistration(services);
            InitCustomControllerRegistration(services);


            return services;
        }

        public static IServiceCollection InitCommonRegistration(IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork<TechCallengeDbContext>));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IService<,>), typeof(Service<,>));
            services.AddTransient(typeof(ControllerBase<,>));

            return services;
        }

        private static IServiceCollection InitCustomRepositoryRegistration(IServiceCollection services)
        {
            return services;
        }

        private static IServiceCollection InitCustomServiceRegistration(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserService), typeof(UserService));
            return services;
        }

        private static IServiceCollection InitCustomControllerRegistration(IServiceCollection services)
        {
            return services;
        }
    }
}
