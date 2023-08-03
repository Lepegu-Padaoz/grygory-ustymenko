using Medical.BL.DTOs;
using Medical.DAL.Entities;

namespace Medical.BL.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task UpdateAsync(UpdatedDoctorDTO updatedDoctor);
        Task AddAsync(AddedDoctorDTO addedDoctor);
        Task DeleteByIdAsync(int id);
    }
}
