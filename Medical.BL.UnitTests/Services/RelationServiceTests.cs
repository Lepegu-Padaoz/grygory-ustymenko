using Medical.BL.Services;
using Medical.BL.Services.Interfaces;
using Medical.DAL.BlobStorage.Interfaces;
using Medical.DAL.Entities;
using Moq;
using NUnit.Framework;

namespace Medical.BL.UnitTests.Services
{
    [TestFixture]
    public class RelationServiceTests
    {
        private RelationService _relationService;
        private Mock<IHospitalService> _hospitalServiceMock;
        private Mock<IDoctorService> _doctorServiceMock;
        private Mock<IBlobStorage> _blobStorageMock;

        [SetUp]
        public void SetUp()
        {
            _hospitalServiceMock = new Mock<IHospitalService>();
            _doctorServiceMock = new Mock<IDoctorService>();
            _blobStorageMock = new Mock<IBlobStorage>();

            _relationService = new RelationService(_hospitalServiceMock.Object, _doctorServiceMock.Object, _blobStorageMock.Object);
        }

        [Test]
        public async Task GetDoctorsByHospitalIdAsync_ShouldReturnListOfDoctors()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctorIds = new List<int> { 1, 2, 3 };

            _blobStorageMock.Setup(bs => bs.FindDoctorByHospitalId(hospitalId)).Returns(doctorIds);

            var doctors = new List<Doctor>
            {
                new Doctor { Id = 1, Name = "Doctor 1" },
                new Doctor { Id = 3, Name = "Doctor 3" }
            };

            _doctorServiceMock.Setup(ds => ds.GetByIdAsync(1)).ReturnsAsync(doctors[0]);
            _doctorServiceMock.Setup(ds => ds.GetByIdAsync(3)).ReturnsAsync(doctors[1]);

            // Act
            var result = await _relationService.GetDoctorsByHospitalIdAsync(hospitalId);

            // Assert
            CollectionAssert.AreEqual(doctors, result.ToList());
        }

        [Test]
        public async Task GetDoctorsByHospitalIdAsync_ShouldReturnEmptyListWhenNoDoctorsFound()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctorIds = new List<int> { 1, 2, 3 };

            _blobStorageMock.Setup(bs => bs.FindDoctorByHospitalId(hospitalId)).Returns(doctorIds);

            // No doctors returned from the doctor service
            _doctorServiceMock.Setup(ds => ds.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Doctor)null);

            // Act
            var result = await _relationService.GetDoctorsByHospitalIdAsync(hospitalId);

            // Assert
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public async Task LinkAsync_ShouldPutContextInBlobStorage_WhenFileDoesNotExist()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctorId = 123;
            var hospital = new Hospital { Id = hospitalId };
            var doctor = new Doctor { Id = doctorId };
            var filename = $"{hospitalId:N}_{doctorId}";

            _hospitalServiceMock.Setup(hs => hs.GetByIdAsync(hospitalId)).ReturnsAsync(hospital);
            _doctorServiceMock.Setup(ds => ds.GetByIdAsync(doctorId)).ReturnsAsync(doctor);

            // File does not exist in BlobStorage
            _blobStorageMock.Setup(bs => bs.ContainsFileByNameAsync(filename)).ReturnsAsync(false);

            // Act
            await _relationService.LinkAsync(hospitalId, doctorId);

            // Assert
            _blobStorageMock.Verify(bs => bs.PutContextAsync(filename), Times.Once);
        }

        [Test]
        public async Task LinkAsync_ShouldNotPutContextInBlobStorage_WhenFileExists()
        {
            // Arrange
            var hospitalId = Guid.NewGuid();
            var doctorId = 123;
            var hospital = new Hospital { Id = hospitalId };
            var doctor = new Doctor { Id = doctorId };
            var filename = $"{hospitalId:N}_{doctorId}";

            _hospitalServiceMock.Setup(hs => hs.GetByIdAsync(hospitalId)).ReturnsAsync(hospital);
            _doctorServiceMock.Setup(ds => ds.GetByIdAsync(doctorId)).ReturnsAsync(doctor);

            // File already exists in BlobStorage
            _blobStorageMock.Setup(bs => bs.ContainsFileByNameAsync(filename)).ReturnsAsync(true);

            // Act
            await _relationService.LinkAsync(hospitalId, doctorId);

            // Assert
            _blobStorageMock.Verify(bs => bs.PutContextAsync(filename), Times.Never);
        }
    }
}
