using Medical.API.Controllers;
using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Medical.DAL.Entities;
using Medical.DAL.Entities.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Medical.API.UnitTests.Controllers
{
    [TestFixture]
    public class DoctorsContollerTests
    {
        private DoctorsController _doctorsController;
        private Mock<ILogger<DoctorsController>> _loggerMock;
        private Mock<IDoctorService> _doctorServiceMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<DoctorsController>>();
            _doctorServiceMock = new Mock<IDoctorService>();
            _doctorsController = new DoctorsController(_loggerMock.Object, _doctorServiceMock.Object);
        }

        [Test]
        public async Task GetAsync_Should_Return_OkResult_With_Doctors()
        {
            // Arrange
            var doctorsList = new List<Doctor>()
            {
                new Doctor() 
                { 
                    Id = 1, 
                    Name = "John Doe", 
                    Specialization = DoctorSpecialization.Pediatrics, 
                    Category = DoctorQualificationCategory.None },
                new Doctor()
                { 
                    Id = 2,
                    Name = "Jane Smith", 
                    Specialization = DoctorSpecialization.Cardiology,
                    Category = DoctorQualificationCategory.Beginner
                }
            };

            _doctorServiceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(doctorsList);

            // Act
            var result = await _doctorsController.GetAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(doctorsList, okResult.Value);
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_OkResult_With_Doctor()
        {
            // Arrange
            int doctorId = 1;
            var doctor = new Doctor()
            {
                Id = 1, 
                Name = "John Doe", 
                Specialization = DoctorSpecialization.Pediatrics,
                Category = DoctorQualificationCategory.None };

            _doctorServiceMock.Setup(s => s.GetByIdAsync(doctorId)).ReturnsAsync(doctor);

            // Act
            var result = await _doctorsController.GetByIdAsync(doctorId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(doctor, okResult.Value);
        }

        [Test]
        public async Task AddSync_Should_Return_OkResult_After_Adding_Doctor()
        {
            // Arrange
            var addedDoctor = new AddedDoctorDTO()
            { 
                Name = "New Doctor Name", 
                Surname = "New Doctor Surname", 
                Category = DoctorQualificationCategory.None, 
                Specialization = DoctorSpecialization.Psychiatry 
            };

            // Act
            var result = await _doctorsController.AddSync(addedDoctor);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            _doctorServiceMock.Verify(s => s.AddAsync(addedDoctor), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_Should_Return_OkResult_After_Updating_Doctor()
        {
            // Arrange
            var updatedDoctor = new UpdatedDoctorDTO() 
            { 
                UpdatedId = 1, 
                Name = "Updated Doctor Name", 
                Surname = "Updated Doctor Surname", 
                Specialization = DoctorSpecialization.Pediatrics, 
                Category = DoctorQualificationCategory.Expert 
            };

            // Act
            var result = await _doctorsController.UpdateAsync(updatedDoctor);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            _doctorServiceMock.Verify(s => s.UpdateAsync(updatedDoctor), Times.Once);
        }

        [Test]
        public async Task DeleteByIdAsync_Should_Return_OkResult_After_Deleting_Doctor()
        {
            // Arrange
            int doctorId = 1;

            // Act
            var result = await _doctorsController.DeleteByIdAsync(doctorId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
            _doctorServiceMock.Verify(s => s.DeleteByIdAsync(doctorId), Times.Once);
        }
    }
}
