using AutoMapper;
using Medical.BL.DTOs;
using Medical.BL.Exceptions;
using Medical.BL.Services.Interfaces;
using Medical.DAL.Entities;
using Medical.DAL.Repositories.Interfaces;

namespace Medical.BL.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository; 
        private readonly IMapper _mapper; 

        public DoctorService(IDoctorRepository doctorRepository, IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper; 
        }

        // Method to add a new doctor using the provided DTO
        public async Task AddAsync(AddedDoctorDTO addedDoctor)
        {
            var doctor = _mapper.Map<AddedDoctorDTO, Doctor>(addedDoctor); // Mapping AddedDoctorDTO to Doctor entity
            await _doctorRepository.AddAsync(doctor); // Saving the new doctor to the database
        }

        // Method to delete a doctor by ID
        public async Task DeleteByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id); // Fetching the doctor by ID from the database

            // Throw EntityNotFoundException if the doctor is not found
            if (doctor is null)
            {
                throw new EntityNotFoundException($"Doctor with ID = {id} is not found! It is impossible to delete!");
            }

            await _doctorRepository.DeleteAsync(doctor); // Deleting the doctor from the database
        }

        // Method to get all doctors
        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _doctorRepository.GetAllAsync(); // Fetching all doctors from the database
        }

        // Method to get a doctor by ID
        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id); // Fetching the doctor by ID from the database
            return doctor;
        }

        // Method to update an existing doctor using the provided DTO
        public async Task UpdateAsync(UpdatedDoctorDTO updatedDoctor)
        {
            var doctor = await _doctorRepository.GetByIdAsync(updatedDoctor.UpdatedId); // Fetching the doctor by ID from the database

            // Throw EntityNotFoundException if the doctor is not found
            if (doctor is null)
            {
                throw new EntityNotFoundException($"Doctor with ID = {updatedDoctor.UpdatedId} is not found! It is impossible to update!");
            }

            _mapper.Map(updatedDoctor, doctor); // Mapping UpdatedDoctorDTO to the existing Doctor entity

            await _doctorRepository.UpdateAsync(doctor); // Saving the updated doctor to the database
        }
    }
}
