using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
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

            // Mapping for EmployeeAdditionalDetails
            CreateMap<EmployeeAdditionalDetailsDto, EmployeeAdditionalDetails>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeBasicDetailsUId, opt => opt.MapFrom(src => src.EmployeeBasicDetailsUId))
                .ForMember(dest => dest.AlternateEmail, opt => opt.MapFrom(src => src.AlternateEmail))
                .ForMember(dest => dest.AlternateMobile, opt => opt.MapFrom(src => src.AlternateMobile))
                .ForMember(dest => dest.WorkInformation, opt => opt.MapFrom(src => src.WorkInformation))
                .ForMember(dest => dest.PersonalDetails, opt => opt.MapFrom(src => src.PersonalDetails))
                .ForMember(dest => dest.IdentityInformation, opt => opt.Ignore());

            CreateMap<EmployeeAdditionalDetails, EmployeeAdditionalDetailsDto>();

            CreateMap<PersonalDetails_, PersonalDetails_>();
            CreateMap<WorkInfo_, WorkInfo_>();
        }

        
    }
}
