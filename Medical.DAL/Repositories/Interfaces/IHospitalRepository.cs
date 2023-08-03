using Medical.DAL.Entities;

namespace Medical.DAL.Repositories.Interfaces
{
    public interface IHospitalRepository
    {
        Task<IEnumerable<Hospital>> GetAllAsync();
        Task<Hospital> GetByIdAsync(Guid id);
        Task UpdateAsync(Hospital hospital);
        Task<Guid> AddAsync(Hospital hospital);
        Task DeleteAsync(Hospital hospital);
    }
}
