using AutoMapper;
using Medical.BL.DTOs;
using Medical.BL.Exceptions;
using Medical.BL.Services;
using Medical.DAL.Entities;
using Medical.DAL.Entities.Enumerations;
using Medical.DAL.Repositories.Interfaces;
using Moq;
using NUnit.Framework;

namespace Medical.BL.UnitTests.Services
{
    [TestFixture]
    public class DoctorServiceTests
    {
        private Mock<IDoctorRepository> _mockDoctorRepository;
        private Mock<IMapper> _mockMapper;
        private DoctorService _doctorService;

        [SetUp]
        public void Setup()
        {
            _mockDoctorRepository = new Mock<IDoctorRepository>();
            _mockMapper = new Mock<IMapper>();

            _doctorService = new DoctorService(_mockDoctorRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task AddAsync_ValidData_SuccessfullyAdds()
        {
            // Arrange
            var addedDoctorDTO = new AddedDoctorDTO() 
            { 
                Name = "TestName", 
                Surname = "TestSurname", 
                Category = DoctorQualificationCategory.Advanced, 
                Specialization = DoctorSpecialization.ENT 
            };
            var doctorEntity = new Doctor()
            { 
                Id = 0, 
                Name = addedDoctorDTO.Name, 
                Specialization = addedDoctorDTO.Specialization, 
                Category = addedDoctorDTO.Category, 
                Surname = addedDoctorDTO.Surname 
            };

            _mockMapper.Setup(m => m.Map<AddedDoctorDTO, Doctor>(It.IsAny<AddedDoctorDTO>())).Returns(doctorEntity);

            // Act
            await _doctorService.AddAsync(addedDoctorDTO);

            // Assert
            _mockDoctorRepository.Verify(repo => repo.AddAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Test]
        public async Task DeleteByIdAsync_DoctorExists_SuccessfullyDeletes()
        {
            // Arrange
            int doctorId = 1;
            var doctorEntity = new Doctor()
            { 
                Id = doctorId,
                Name = "TestName",
                Surname = "TestSurname",
                Category = DoctorQualificationCategory.Advanced,
                Specialization = DoctorSpecialization.ENT
            };
            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync(doctorEntity);

            // Act
            await _doctorService.DeleteByIdAsync(doctorId);

            // Assert
            _mockDoctorRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Test]
        public void DeleteByIdAsync_DoctorDoesNotExist_ThrowsEntityNotFoundException()
        {
            // Arrange
            int doctorId = 1;
            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync((Doctor)null);

            // Act & Assert
            Assert.ThrowsAsync<EntityNotFoundException>(() => _doctorService.DeleteByIdAsync(doctorId));
        }

        [Test]
        public async Task GetAllAsync_ReturnsListOfDoctors()
        {
            // Arrange
            var doctorList = new List<Doctor>()
            { 
                new Doctor() 
                {
                    Id = 1,
                    Name = "TestName1",
                    Surname = "TestSurname1",
                    Category = DoctorQualificationCategory.Advanced,
                    Specialization = DoctorSpecialization.ENT
                },
                new Doctor()
                {
                    Id = 1,
                    Name = "TestName2",
                    Surname = "TestSurname2",
                    Category = DoctorQualificationCategory.None,
                    Specialization = DoctorSpecialization.Cardiology
                }
            };

            _mockDoctorRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(doctorList);

            // Act
            var result = await _doctorService.GetAllAsync();

            // Assert
            Assert.AreEqual(doctorList, result);
        }

        [Test]
        public async Task GetByIdAsync_DoctorExists_ReturnsDoctor()
        {
            // Arrange
            int doctorId = 1;
            var doctorEntity = new Doctor()
            { 
                Id = doctorId,
                Name = "TestName1",
                Surname = "TestSurname1",
                Category = DoctorQualificationCategory.Advanced,
                Specialization = DoctorSpecialization.ENT
            };

            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync(doctorEntity);

            // Act
            var result = await _doctorService.GetByIdAsync(doctorId);

            // Assert
            Assert.AreEqual(doctorEntity, result);
        }

        [Test]
        public async Task GetByIdAsync_DoctorDoesNotExist_ReturnsNull()
        {
            // Arrange
            int doctorId = 1;
            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync((Doctor)null);

            // Act
            var result = await _doctorService.GetByIdAsync(doctorId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateAsync_DoctorExists_SuccessfullyUpdates()
        {
            // Arrange
            int doctorId = 1;
            var updatedDoctorDTO = new UpdatedDoctorDTO() 
            { 
                UpdatedId = doctorId, 
                Name = "TestName", 
                Surname = "TestSurname", 
                Category = DoctorQualificationCategory.Beginner, 
                Specialization = DoctorSpecialization.GeneralMedicine 
            };
            var doctorEntity = new Doctor()
            { 
                Id = doctorId,
                Name = "TestName1",
                Surname = "TestSurname1",
                Category = DoctorQualificationCategory.Expert,
                Specialization = DoctorSpecialization.Radiology
            };

            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync(doctorEntity);

            // Act
            await _doctorService.UpdateAsync(updatedDoctorDTO);

            // Assert
            _mockMapper.Verify(mapper => mapper.Map(updatedDoctorDTO, doctorEntity), Times.Once);
            _mockDoctorRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Doctor>()), Times.Once);
        }

        [Test]
        public void UpdateAsync_DoctorDoesNotExist_ThrowsEntityNotFoundException()
        {
            // Arrange
            int doctorId = 1;
            var updatedDoctorDTO = new UpdatedDoctorDTO()
            { 
                UpdatedId = doctorId,
                Name = "TestName1",
                Surname = "TestSurname1",
                Category = DoctorQualificationCategory.Expert,
                Specialization = DoctorSpecialization.Radiology
            };

            _mockDoctorRepository.Setup(repo => repo.GetByIdAsync(doctorId)).ReturnsAsync((Doctor)null);

            //  Assert
            Assert.ThrowsAsync<EntityNotFoundException>(() => _doctorService.UpdateAsync(updatedDoctorDTO));
        }
    }
}
