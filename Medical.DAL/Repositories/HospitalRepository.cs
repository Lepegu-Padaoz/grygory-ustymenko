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

        // Adding hospital entity into cosmos database and saving changes
        public async Task<Guid> AddAsync(Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();

            return hospital.Id;
        }

        // Deleting hospital entity into cosmos database and saving changes
        public async Task DeleteAsync(Hospital hospital)
        {
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
        }

        // Retrieving all hospital entities into cosmos database and saving changes
        public async Task<IEnumerable<Hospital>> GetAllAsync()
        {
            return await _context.Hospitals.ToListAsync();
        }

        // Retrieving hospital entity by hospital id
        public async Task<Hospital> GetByIdAsync(Guid id)
        {
            return await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == id);
        }

        // Updating hospital entity into cosmos cosmos database and saving changes
        public async Task UpdateAsync(Hospital hospital)
        {
            _context.Hospitals.Update(hospital);
            await _context.SaveChangesAsync();
        }
    }
}
