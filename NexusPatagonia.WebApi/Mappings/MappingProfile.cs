using AutoMapper;
using NexusPatagonia.Domain.DTOs;
using NexusPatagonia.Requests;

namespace NexusPatagonia.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CashMovementRequest, CashMovementSaveDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.Invoiced, opt => opt.MapFrom(src => src.Invoiced))
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.SubcategoryId, opt => opt.MapFrom(src => src.SubcategoryId))
            .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));
        }
    }
}