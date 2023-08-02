using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medical.DAL.Entities;
using Medical.DAL.Entities.Enumerations;
using Medical.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Medical.DAL.UnitTests.Repositories
{
    [TestFixture]
    public class DoctorRepositoryTests
    {
        private MedicalContext _context;
        private DoctorRepository _doctorRepository;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<MedicalContext>()
                .UseInMemoryDatabase(databaseName: "Test_MedicalDatabase")
                .Options;

            _context = new MedicalContext(options);
            _doctorRepository = new DoctorRepository(_context);
        }

        [SetUp]
        public async Task Setup()
        {
            await ClearDoctorsTableIntoDatabase();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddAsync_ShouldAddDoctorToDatabase()
        {
            // Arrange
            var doctor = new Doctor { Name = "John", Surname = "Doe", Category = DoctorQualificationCategory.Beginner, Specialization = DoctorSpecialization.Pediatrics };

            // Act
            await _doctorRepository.AddAsync(doctor);

            // Assert
            var result = await _context.Doctors.FindAsync(doctor.Id);
            Assert.NotNull(result);
            Assert.AreEqual(doctor.Name, result.Name);
            Assert.AreEqual(doctor.Category, result.Category);
        }

        [Test]
        public async Task DeleteAsync_ShouldDeleteDoctorFromDatabase()
        {
            // Arrange
            var doctor = new Doctor { Name = "John", Surname = "Doe", Category = DoctorQualificationCategory.Advanced, Specialization = DoctorSpecialization.Pediatrics };
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            // Act
            await _doctorRepository.DeleteAsync(doctor);

            // Assert
            var result = await _context.Doctors.FindAsync(doctor.Id);
            Assert.Null(result);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllDoctors()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Name = "John", Surname = "Doe", Category = DoctorQualificationCategory.Advanced, Specialization = DoctorSpecialization.Pediatrics  },
                new Doctor { Name = "Jane", Surname = "Smith", Category = DoctorQualificationCategory.None, Specialization = DoctorSpecialization.Pediatrics  },
                new Doctor { Name = "Mike", Surname = "Johnson", Category = DoctorQualificationCategory.Beginner, Specialization = DoctorSpecialization.Pediatrics  }
            };
            await _context.Doctors.AddRangeAsync(doctors);
            await _context.SaveChangesAsync();

            // Act
            var result = await _doctorRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(doctors.Count, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectDoctor()
        {
            // Arrange
            var doctors = new List<Doctor>
            {
                new Doctor { Name = "John", Surname = "Doe", Category = DoctorQualificationCategory.Advanced, Specialization = DoctorSpecialization.Pediatrics  },
                new Doctor { Name = "Jane", Surname = "Smith", Category = DoctorQualificationCategory.None, Specialization = DoctorSpecialization.Pediatrics  },
                new Doctor { Name = "Mike", Surname = "Johnson", Category = DoctorQualificationCategory.Beginner, Specialization = DoctorSpecialization.Pediatrics  }
            };
            await _context.Doctors.AddRangeAsync(doctors);
            await _context.SaveChangesAsync();
            var targetDoctor = doctors[1]; // Choose the second doctor for testing

            // Act
            var result = await _doctorRepository.GetByIdAsync(targetDoctor.Id);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(targetDoctor.Name, result.Name);
            Assert.AreEqual(targetDoctor.Category, result.Category);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateDoctorInDatabase()
        {
            // Arrange
            var doctor = new Doctor { Name = "John", Surname = "Doe", Category = DoctorQualificationCategory.None, Specialization = DoctorSpecialization.Pediatrics };
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            _context.Entry(doctor).State = EntityState.Detached;

            var updatedDoctor = new Doctor { Id = doctor.Id, Name = "John", Surname = "Smith", Category = DoctorQualificationCategory.Intermediate, Specialization = DoctorSpecialization.Pediatrics };

            // Act
            await _doctorRepository.UpdateAsync(updatedDoctor);

            // Assert
            var result = await _context.Doctors.FindAsync(doctor.Id);
            Assert.NotNull(result);
            Assert.AreEqual(updatedDoctor.Name, result.Name);
            Assert.AreEqual(updatedDoctor.Category, result.Category);
        }


        private async Task ClearDoctorsTableIntoDatabase()
        {
            var allDoctors = _context.Doctors.ToList();
            _context.Doctors.RemoveRange(allDoctors);
            await _context.SaveChangesAsync();
        }
    }
}
