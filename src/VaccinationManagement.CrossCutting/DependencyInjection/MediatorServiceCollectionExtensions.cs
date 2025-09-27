using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace VaccinationManagement.CrossCutting.DependencyInjection
{
    public static class MediatorServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.Load("VaccinationManagement.Application")));

            return services;
        }
    }
}
