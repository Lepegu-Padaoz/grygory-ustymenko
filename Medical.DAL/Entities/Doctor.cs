using Medical.DAL.Entities.Enumerations;

namespace Medical.DAL.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DoctorQualificationCategory Category { get; set; }
        public DoctorSpecialization Specialization { get; set; }
    }
}
