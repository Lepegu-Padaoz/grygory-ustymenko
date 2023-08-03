using AutoMapper;
using Medical.BL.DTOs;
using Medical.BL.Exceptions;
using Medical.BL.Services;
using Medical.DAL.Entities;
using Medical.DAL.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace Medical.BL.UnitTests.Services
{
    [TestFixture]
    public class HospitalServiceTests
    {
        private HospitalService _hospitalService;
        private Mock<IHospitalRepository> _hospitalRepositoryMock;
        private Mock<IMapper> _mapperMock;

        [SetUp]
        public void SetUp()
        {
            _hospitalRepositoryMock = new Mock<IHospitalRepository>();
            _mapperMock = new Mock<IMapper>();
            _hospitalService = new HospitalService(_hospitalRepositoryMock.Object, _mapperMock.Object);
        }

        // Test for AddAsync method
        [Test]
        public async Task AddAsync_ShouldReturnHospitalIdOnSuccess()
        {
            // Arrange
            var addedHospitalDTO = new AddedHospitalDTO { Name = "New Hospital", Address = "New Address" };
            var addedHospital = new Hospital { Id = Guid.NewGuid(), Name = "New Hospital", Address = "New Address" };
            var hospitalId = Guid.NewGuid();

            _mapperMock.Setup(x => x.Map<AddedHospitalDTO, Hospital>(addedHospitalDTO)).Returns(addedHospital);
            _hospitalRepositoryMock.Setup(x => x.AddAsync(addedHospital)).ReturnsAsync(hospitalId);

            // Act
            var result = await _hospitalService.AddAsync(addedHospitalDTO);

            // Assert
            Assert.AreEqual(hospitalId, result);
        }

        // Test for DeleteByIdAsync method - Successful deletion
        [Test]
        public async Task DeleteByIdAsync_ShouldDeleteHospitalById()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var existingHospital = new Hospital { Id = hospitalId, Name = "Existing Hospital", Address = "Existing Address" };

            _hospitalRepositoryMock.Setup(x => x.GetByIdAsync(hospitalId)).ReturnsAsync(existingHospital);

            // Act
            await _hospitalService.DeleteByIdAsync(hospitalId);

            // Assert
            _hospitalRepositoryMock.Verify(x => x.DeleteAsync(existingHospital), Times.Once);
        }

        // Test for DeleteByIdAsync method - Hospital not found
        [Test]
        public void DeleteByIdAsync_ThrowsEntityNotFoundExceptionWhenHospitalNotFound()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();

            _hospitalRepositoryMock.Setup(x => x.GetByIdAsync(hospitalId)).ReturnsAsync((Hospital)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _hospitalService.DeleteByIdAsync(hospitalId));
        }

        // Test for GetAllAsync method
        [Test]
        public async Task GetAllAsync_ShouldReturnListOfHospitals()
        {
            // Arrange
            var hospitals = new List<Hospital>
            {
                new Hospital { Id = Guid.NewGuid(), Name = "Hospital 1", Address = "Address 1" },
                new Hospital { Id = Guid.NewGuid(), Name = "Hospital 2", Address = "Address 2" }
            };

            _hospitalRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(hospitals);

            // Act
            var result = await _hospitalService.GetAllAsync();

            // Assert
            Assert.AreEqual(hospitals, result);
        }

        // Test for GetByIdAsync method
        [Test]
        public async Task GetByIdAsync_ShouldReturnHospitalById()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var hospital = new Hospital { Id = hospitalId, Name = "Test Hospital", Address = "Test Address" };

            _hospitalRepositoryMock.Setup(x => x.GetByIdAsync(hospitalId)).ReturnsAsync(hospital);

            // Act
            var result = await _hospitalService.GetByIdAsync(hospitalId);

            // Assert
            Assert.AreEqual(hospital, result);
        }

        // Test for UpdateAsync method
        [Test]
        public async Task UpdateAsync_ShouldUpdateHospitalSuccessfully()
        {
            // Arrange
            var updatedHospitalDTO = new UpdatedHospitalDTO { UpdatedId = Guid.NewGuid(), Name = "Updated Hospital", Address = "Updated Address" };
            var existingHospital = new Hospital { Id = updatedHospitalDTO.UpdatedId, Name = "Existing Hospital", Address = "Existing Address" };

            _hospitalRepositoryMock.Setup(x => x.GetByIdAsync(updatedHospitalDTO.UpdatedId)).ReturnsAsync(existingHospital);

            // Act
            await _hospitalService.UpdateAsync(updatedHospitalDTO);

            // Assert
            _mapperMock.Verify(x => x.Map(updatedHospitalDTO, existingHospital), Times.Once);
            _hospitalRepositoryMock.Verify(x => x.UpdateAsync(existingHospital), Times.Once);
        }

        // Test for UpdateAsync method - Hospital not found
        [Test]
        public void UpdateAsync_ThrowsEntityNotFoundExceptionWhenHospitalNotFound()
        {
            // Arrange
            var updatedHospitalDTO = new UpdatedHospitalDTO { UpdatedId = Guid.NewGuid(), Name = "Updated Hospital", Address = "Updated Address" };

            _hospitalRepositoryMock.Setup(x => x.GetByIdAsync(updatedHospitalDTO.UpdatedId)).ReturnsAsync((Hospital)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(async () => await _hospitalService.UpdateAsync(updatedHospitalDTO));
        }
    }
}
