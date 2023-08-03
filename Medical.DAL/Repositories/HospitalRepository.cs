using Medical.DAL.Entities;
using Medical.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Medical.DAL.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly CosmosContext _context;

        public HospitalRepository(CosmosContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddAsync(Hospital hospital)
        {
            _context.Hospitals?.Add(hospital);
            await _context.SaveChangesAsync();

            return hospital.Id;
        }

        public async Task DeleteAsync(Hospital hospital)
        {
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        public async Task<Hospital> GetByIdAsync(Guid id)
        {
            return await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Hospital hospital)
        {
            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();
        }
    }
}
