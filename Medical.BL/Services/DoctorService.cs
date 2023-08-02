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
        public async Task AddAsync(AddedDoctorDTO addedDoctor)
        {   
            var doctor = _mapper.Map<AddedDoctorDTO, Doctor>(addedDoctor);
            await _doctorRepository.AddAsync(doctor);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor is null)
            {
                throw new EntityNotFoundException($"Doctor with ID = {id} is not found! It is impossible to delete!");
            }

            await _doctorRepository.DeleteAsync(doctor);
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _doctorRepository.GetAllAsync();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            return doctor;
        }

        public async Task UpdateAsync(UpdatedDoctorDTO updatedDoctor)
        {
            var doctor = await _doctorRepository.GetByIdAsync(updatedDoctor.UpdatedId);
            
            if (doctor is null)
            {
                throw new EntityNotFoundException($"Doctor with ID = {updatedDoctor.UpdatedId} is not found! It is impossible to update!");
            }

            _mapper.Map(updatedDoctor, doctor);

            await _doctorRepository.UpdateAsync(doctor);
        }
    }
}
