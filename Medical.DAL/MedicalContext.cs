using Medical.DAL.Entities;
using Medical.DAL.Entities.Enumerations;
using Medical.DAL.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Medical.DAL
{
    public class MedicalContext : DbContext
    {
        public MedicalContext(DbContextOptions<MedicalContext> options) : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Some settings to write enum into database not in INT type but in NVARCHAR type (for example: not 0,1,etc but Orthopedics, Cardiology, etc) 
            modelBuilder
                .Entity<Doctor>()
                .Property(e => e.Category)
                .HasConversion(new EnumToStringConverter<DoctorQualificationCategory>());

            modelBuilder
                .Entity<Doctor>()
                .Property(e => e.Specialization)
                .HasConversion(new EnumToStringConverter<DoctorSpecialization>());
        }
    }
}
