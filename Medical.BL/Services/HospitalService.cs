using AutoMapper;
using Medical.BL.DTOs;
using Medical.BL.Exceptions;
using Medical.BL.Services.Interfaces;
using Medical.DAL.Entities;
using Medical.DAL.Repositories.Interfaces;

namespace Medical.BL.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository; 
        private readonly IMapper _mapper; 

        public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper; 
        }

        // Method to add a new hospital using the provided DTO
        public async Task<Guid> AddAsync(AddedHospitalDTO addedHospital)
        {
            var hospital = _mapper.Map<AddedHospitalDTO, Hospital>(addedHospital);
            var hospitalId = await _hospitalRepository.AddAsync(hospital);

            return hospitalId;
        }

        // Method to delete a hospital by ID
        public async Task DeleteByIdAsync(Guid id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id); // Fetching the hospital by ID from the database

            // Throw EntityNotFoundException if the hospital is not found
            if (hospital is null)
            {
                throw new EntityNotFoundException($"Hospital with ID = {id} is not found! It is impossible to delete!");
            }

            await _hospitalRepository.DeleteAsync(hospital); // Deleting the hospital from the database
        }

        // Method to get all Hospitals
        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _hospitalRepository.GetAllAsync(); // Fetching all Hospitals from the database
        }

        // Method to get a hospital by ID
        public async Task<Hospital> GetByIdAsync(Guid id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id); // Fetching the hospital by ID from the database
            return hospital;
        }

        // Method to update an existing hospital using the provided DTO
        public async Task UpdateAsync(UpdatedHospitalDTO updatedHospital)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(updatedHospital.UpdatedId); // Fetching the hospital by ID from the database

            // Throw EntityNotFoundException if the hospital is not found
            if (hospital is null)
            {
                throw new EntityNotFoundException($"Hospital with ID = {updatedHospital.UpdatedId} is not found! It is impossible to update!");
            }

            _mapper.Map(updatedHospital, hospital); // Mapping UpdatedHospitalDTO to the existing Hospital entity

            await _hospitalRepository.UpdateAsync(hospital); // Saving the updated hospital to the database
        }
    }
}
