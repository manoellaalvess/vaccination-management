using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VaccinationManagement.Domain.Repository;
using VaccinationManagement.Infrastructure.Repositories;
using VaccinationManagement.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace VaccinationManagement.CrossCutting.DependencyInjection
{
    public static class RepositoriesServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VaccinationDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IVaccineRepository, VaccineRepository>();
            services.AddScoped<IVaccinationRepository, VaccinationRepository>();

            return services;
        }
    }
}