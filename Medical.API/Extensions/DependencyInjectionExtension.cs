﻿using Medical.BL.Services;  
using Medical.BL.Services.Interfaces;
using Medical.DAL; 
using Medical.DAL.Repositories; 
using Medical.DAL.Repositories.Interfaces;

namespace Medical.API.Extensions
{
    public static class DependencyInjectionExtension
    {
        // Method to register DataAccessLayer dependencies
        public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            // Registering the MedicalContext using a connection string from configuration
            services.AddSqlServer<MedicalContext>(configuration.GetConnectionString("DatabaseConnectionString"));
        }

        // Method to register BusinessLayer dependencies
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
        }
    }
}
