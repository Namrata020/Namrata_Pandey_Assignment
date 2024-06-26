using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Common
{
    public class BasicMappingProfile : Profile
    {
        public BasicMappingProfile()
        {
            // Mapping for EmployeeBasicDetails
            CreateMap<EmployeeBasicDetailsDto, EmployeeBasicDetails>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UId))
                .ForMember(dest => dest.UId, opt => opt.MapFrom(src => src.UId))
                .ForMember(dest => dest.DocumentType, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedOn, opt => opt.Ignore())
                .ForMember(dest => dest.Version, opt => opt.Ignore())
                .ForMember(dest => dest.Active, opt => opt.Ignore())
                .ForMember(dest => dest.Archived, opt => opt.Ignore());

            CreateMap<EmployeeBasicDetails, EmployeeBasicDetailsDto>();

        }

        
    }
}
