using Medical.DAL.Entities;
using Medical.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Medical.DAL.UnitTests.Repositories
{
    [TestFixture]
    public class HospitalRepositoryTests
    {
        private HospitalRepository _hospitalRepository;
        private CosmosContext _context;
        private List<Hospital> _dummyHospitals = new List<Hospital>()
        {
            new Hospital { Id = Guid.NewGuid(), Name = "Hospital 1", Address = "Address 1" },
            new Hospital { Id = Guid.NewGuid(), Name = "Hospital 2", Address = "Address 2" },
            new Hospital { Id = Guid.NewGuid(), Name = "Hospital 3", Address = "Address 3" },
        };

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var options = new DbContextOptionsBuilder<CosmosContext>()
                .UseInMemoryDatabase(databaseName: "Test_Cosmos_MedicalDatabase")
                .Options;

            _context = new CosmosContext(options);
            _hospitalRepository = new HospitalRepository(_context);

            _context.AddRange(_dummyHospitals);
        }

        [SetUp]
        public void TearDown()
        {
            _context.RemoveRange(_context.Hospitals.ToList());
        }

        [Test]
        public async Task AddAsync_ShouldAddHospitalToContext()
        {
            // Arrange
            var newHospital = new Hospital { Id = Guid.NewGuid(), Name = "New Hospital", Address = "Address" };

            // Act
            await _hospitalRepository.AddAsync(newHospital);

            // Assert
            Assert.IsTrue(_context.Hospitals.Contains(newHospital));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveHospitalFromContext()
        {
            // Arrange
            var hospitalToDelete = _dummyHospitals[0];

            // Act
            await _hospitalRepository.DeleteAsync(hospitalToDelete);

            // Assert
            Assert.IsFalse(_context.Hospitals.Contains(hospitalToDelete));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllHospitals()
        {
            // Arrange
            _context.Hospitals.AddRange(_dummyHospitals);
            await _context.SaveChangesAsync();
            // Act
            var result = await _hospitalRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(_dummyHospitals.Count, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnHospitalById()
        {
            // Arrange
            var hospital = new Hospital()
            {
                Name = "UHospital77",
                Address = "UAddress77"
            };
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();

            var hospitalToFind = hospital;

            // Act
            var result = await _hospitalRepository.GetByIdAsync(hospitalToFind.Id);

            // Assert
            Assert.AreEqual(hospitalToFind, result);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateHospitalInContext()
        {
            // Arrange
            var hospital = new Hospital()
            {
                Name = "UHospital",
                Address = "UAddress"
            };
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();

            var hospitalToUpdate = hospital;

            // Modify some properties of the hospital
            var updatedName = "Updated Hospital";
            hospitalToUpdate.Name = updatedName;

            // Act
            await _hospitalRepository.UpdateAsync(hospitalToUpdate);

            // Assert
            Assert.AreEqual(updatedName, _context.Hospitals.FirstOrDefault(x => x.Id == hospitalToUpdate.Id)?.Name);
        }   
    }
}
