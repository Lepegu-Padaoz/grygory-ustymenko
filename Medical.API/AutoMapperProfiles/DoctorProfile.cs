using AutoMapper;
using Medical.BL.DTOs;
using Medical.DAL.Entities;

namespace Medical.API.AutoMapperProfiles
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {
            CreateMap<AddedDoctorDTO, Doctor>().ReverseMap();
            CreateMap<UpdatedDoctorDTO, Doctor>().ReverseMap();
        }
    }
}
