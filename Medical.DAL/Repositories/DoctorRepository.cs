using Medical.DAL.Entities;
using Medical.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medical.DAL.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MedicalContext _context;

        public DoctorRepository(MedicalContext context)
        {
            _context = context;
        }

        // Adding doctor entity into database and saving changes
        public async Task AddAsync(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        // Deleting doctor entity into database and saving changes
        public async Task DeleteAsync(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }

        // Retrieving all doctor entities into database and saving changes
        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _context.Doctors.AsNoTracking().ToListAsync();
        }

        // Retrieving doctor entity by doctor id
        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        // Updating doctor entity into database and saving changes
        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Update(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
