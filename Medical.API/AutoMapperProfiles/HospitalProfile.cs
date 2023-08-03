using AutoMapper;
using Medical.BL.DTOs;
using Medical.DAL.Entities;

namespace Medical.API.AutoMapperProfiles
{
    /// Profile to map Hospital entity to DTO
    public class HospitalProfile : Profile
    {
        public HospitalProfile()
        {
            CreateMap<AddedHospitalDTO, Hospital>().ReverseMap();
            CreateMap<UpdatedHospitalDTO, Hospital>().ReverseMap();
        }
    }
}
