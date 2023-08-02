using Medical.DAL.Entities;

namespace Medical.DAL.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor> GetByIdAsync(int id);
        Task UpdateAsync(Doctor doctor);
        Task AddAsync(Doctor doctor);
        Task DeleteAsync(Doctor doctor);
    }
}
