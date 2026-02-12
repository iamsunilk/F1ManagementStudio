using AutoMapper;
using F1ManagementStudioWebAPI.Models.Domain;
using F1ManagementStudioWebAPI.Models.DTOs.CarDtos;

namespace F1ManagementStudioWebAPI.Mapping
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        { 

            CreateMap<Car, CarDto>()
             .ReverseMap();
        }
    }
}
