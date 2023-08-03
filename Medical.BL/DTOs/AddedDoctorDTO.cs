using Medical.DAL.Entities.Enumerations;

namespace Medical.BL.DTOs
{
    public class AddedDoctorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DoctorQualificationCategory Category { get; set; }
        public DoctorSpecialization Specialization { get; set; }
    }
}
