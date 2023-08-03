using Medical.API.Controllers;
using Medical.BL.DTOs;
using Medical.BL.Services.Interfaces;
using Medical.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Medical.API.UnitTests.Controllers
{
    [TestFixture]
    public class HospitalsControllerTests
    {
        private HospitalsController _controller;
        private Mock<IHospitalService> _hospitalServiceMock;

        [SetUp]
        public void SetUp()
        {
            _hospitalServiceMock = new Mock<IHospitalService>();
            _controller = new HospitalsController(_hospitalServiceMock.Object);
        }

        // Test for GetAsync method
        [Test]
        public async Task GetAsync_ShouldReturnListOfHospitals()
        {
            // Arrange
            var hospitals = new List<Hospital>
            {
                new Hospital { Id = Guid.NewGuid(), Name = "Hospital 1", Address = "Address 1" },
                new Hospital { Id = Guid.NewGuid(), Name = "Hospital 2", Address = "Address 2" }
            };

            _hospitalServiceMock.Setup(x => x.GetAllAsync()).ReturnsAsync(hospitals);

            // Act
            var result = await _controller.GetAsync();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(hospitals, okResult?.Value);
        }

        // Test for GetByIdAsync method
        [Test]
        public async Task GetByIdAsync_ShouldReturnHospitalById()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var hospital = new Hospital { Id = hospitalId, Name = "Test Hospital", Address = "Test Address" };

            _hospitalServiceMock.Setup(x => x.GetByIdAsync(hospitalId)).ReturnsAsync(hospital);

            // Act
            var result = await _controller.GetByIdAsync(hospitalId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(hospital, okResult?.Value);
        }

        // Test for AddSync method
        [Test]
        public async Task AddSync_ShouldReturnHospitalIdOnSuccess()
        {
            // Arrange
            var addedHospital = new AddedHospitalDTO { Name = "New Hospital", Address = "New Address" };
            var hospitalId = Guid.NewGuid();

            _hospitalServiceMock.Setup(x => x.AddAsync(addedHospital)).ReturnsAsync(hospitalId);

            // Act
            var result = await _controller.AddSync(addedHospital);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(hospitalId, okResult?.Value);
        }

        // Test for UpdateAsync method
        [Test]
        public async Task UpdateAsync_ShouldReturnOkOnSuccess()
        {
            // Arrange
            var updatedHospital = new UpdatedHospitalDTO { UpdatedId = Guid.NewGuid(), Name = "Updated Hospital", Address = "Updated Address" };

            // Act
            var result = await _controller.UpdateAsync(updatedHospital);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        // Test for DeleteByIdAsync method
        [Test]
        public async Task DeleteByIdAsync_ShouldReturnOkOnSuccess()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();

            // Act
            var result = await _controller.DeleteByIdAsync(hospitalId);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
