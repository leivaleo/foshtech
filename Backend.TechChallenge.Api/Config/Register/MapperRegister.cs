using AutoMapper;
using Backend.TechChallenge.Api.Config.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Api.Config.Register
{
    public class MapperRegister
    {
        public static IServiceCollection InitRegistration(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ApplicationMapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
