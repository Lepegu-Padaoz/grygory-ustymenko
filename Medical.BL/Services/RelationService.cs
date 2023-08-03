using Medical.BL.Services.Interfaces;
using Medical.DAL.BlobStorage.Interfaces;
using Medical.DAL.Entities;

namespace Medical.BL.Services
{
    /// <summary>
    /// Service class responsible for handling relations between hospitals and doctors.
    /// </summary>
    public class RelationService : IRelationService
    {
        private readonly IHospitalService _hospitalService;
        private readonly IDoctorService _doctorService;
        private readonly IBlobStorage _blobStorage;

        /// <summary>
        /// Constructor to initialize the RelationService with required dependencies.
        /// </summary>
        /// <param name="hospitalService">The hospital service used to fetch hospital information.</param>
        /// <param name="doctorService">The doctor service used to fetch doctor information.</param>
        /// <param name="blobStorage">The BlobStorage service used to interact with Azure Blob Storage.</param>
        public RelationService(IHospitalService hospitalService, IDoctorService doctorService, IBlobStorage blobStorage)
        {
            _hospitalService = hospitalService;
            _doctorService = doctorService;
            _blobStorage = blobStorage;
        }

        /// <summary>
        /// Get a list of doctors associated with a specific hospital.
        /// </summary>
        /// <param name="hospitalId">The unique identifier of the hospital.</param>
        /// <returns>A collection of Doctor entities associated with the hospital.</returns>
        public async Task<IEnumerable<Doctor>> GetDoctorsByHospitalIdAsync(Guid hospitalId)
        {
            // Get the list of doctor IDs associated with the hospital from BlobStorage.
            var doctorIds = _blobStorage.FindDoctorByHospitalId(hospitalId);

            var doctors = new List<Doctor>();
            foreach (var id in doctorIds)
            {
                var doctor = await _doctorService.GetByIdAsync(id);

                if (doctor is not null)
                {
                    doctors.Add(doctor);
                }
            }

            return doctors;
        }

        /// <summary>
        /// Link a doctor to a hospital by updating the BlobStorage with the association.
        /// </summary>
        /// <param name="hospitalId">The unique identifier of the hospital.</param>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        public async Task LinkAsync(Guid hospitalId, int doctorId)
        {
            var hospital = await _hospitalService.GetByIdAsync(hospitalId);
            var doctor = await _doctorService.GetByIdAsync(doctorId);

            var filename = $"{hospitalId:N}_{doctorId}";

            var isExists = await _blobStorage.ContainsFileByNameAsync(filename);

            // If the association doesn't exist, create it in BlobStorage by putting the context.
            if (!isExists)
            {
                await _blobStorage.PutContextAsync(filename);
            }
        }
    }
}
