using Medical.DAL.Entities;

namespace Medical.BL.Services.Interfaces
{
    public interface IRelationService
    {
        Task LinkAsync(Guid hospitalId, int doctorId);
        Task<IEnumerable<Doctor>> GetDoctorsByHospitalIdAsync(Guid hospitalId);
    }
}
