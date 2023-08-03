using Medical.API.Controllers;
using Medical.BL.Services.Interfaces;
using Medical.DAL.Entities;
using Medical.DAL.Entities.Enumerations;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Medical.API.UnitTests.Controllers
{
    [TestFixture]
    public class RelationsControllerTests
    {
        private RelationsController _controller;
        private Mock<IRelationService> _relationServiceMock;

        [SetUp]
        public void SetUp()
        {
            _relationServiceMock = new Mock<IRelationService>();
            _controller = new RelationsController(_relationServiceMock.Object);
        }

        // Test for LinkAsync method
        [Test]
        public async Task LinkAsync_ShouldReturnOkOnSuccess()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctorId = 123;

            // Act
            var result = await _controller.LinkAsync(hospitalId, doctorId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        // Test for GetDoctorsByHospitalIdAsync method
        [Test]
        public async Task GetDoctorsByHospitalIdAsync_ShouldReturnListOfDoctors()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "Doctor 1", Specialization = DoctorSpecialization.Psychiatry, Category = DoctorQualificationCategory.Intermediate },
                new Doctor { Id = 2, Name = "Doctor 2", Specialization = DoctorSpecialization.Psychiatry, Category = DoctorQualificationCategory.Intermediate }
            };

            _relationServiceMock.Setup(x => x.GetDoctorsByHospitalIdAsync(hospitalId)).ReturnsAsync(doctors);

            // Act
            var result = await _controller.GetDoctorsByHospitalIdAsync(hospitalId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(doctors, okResult?.Value);
        }
    }
}
