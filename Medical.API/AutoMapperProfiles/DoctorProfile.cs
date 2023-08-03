using AutoMapper;
using Medical.BL.DTOs;
using Medical.DAL.Entities;

namespace Medical.API.AutoMapperProfiles
{
    /// Profile to map Doctor entity to DTO
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<AddedDoctorDTO, Doctor>().ReverseMap();
            CreateMap<UpdatedDoctorDTO, Doctor>().ReverseMap();
        }
    }
}
