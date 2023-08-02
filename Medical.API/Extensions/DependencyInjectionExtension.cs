using Medical.BL.Services;
using Medical.BL.Services.Interfaces;
using Medical.DAL;
using Medical.DAL.Repositories;
using Medical.DAL.Repositories.Interfaces;

namespace Medical.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            services.AddSqlServer<MedicalContext>(configuration.GetConnectionString("DatabaseConnectionString"));
        }

        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
