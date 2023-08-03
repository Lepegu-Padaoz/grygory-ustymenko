using Medical.BL.Services;  
using Medical.BL.Services.Interfaces;
using Medical.DAL;
using Medical.DAL.BlobStorage;
using Medical.DAL.BlobStorage.Interfaces;
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
            services.AddScoped<IHospitalRepository, HospitalRepository>();

            // Registering the MedicalContext using a connection string from configuration
            services.AddSqlServer<MedicalContext>(configuration.GetConnectionString("DatabaseConnectionString"));
            // Registering the CosmosContext using a connection string from configuration
            services.AddCosmos<CosmosContext>(configuration.GetConnectionString("CosmosConnectionString")!, configuration.GetConnectionString("CosmosDatabaseName")!);
            // Registering Azure Blob Storage
            var blobStorageConnectionString = configuration.GetConnectionString("AzureBlobStorageConnectionString");
            var config = new BlobConfiguration(blobStorageConnectionString);

            services.AddSingleton(config);
            services.AddScoped<IBlobStorage, BlobStorage>();
        }

        // Method to register BusinessLayer dependencies
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IHospitalService, HospitalService>();
            services.AddScoped<IRelationService, RelationService>();
        }
    }
}
