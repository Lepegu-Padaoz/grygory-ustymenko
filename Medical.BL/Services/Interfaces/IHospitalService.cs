using Medical.BL.DTOs;
using Medical.DAL.Entities;

namespace Medical.BL.Services.Interfaces
{
    public interface IHospitalService
    {
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Hospital> GetByIdAsync(Guid id);
        Task UpdateAsync(UpdatedHospitalDTO updatedHospital);
        Task<Guid> AddAsync(AddedHospitalDTO addedHospital);
        Task DeleteByIdAsync(Guid id);
    }
}
