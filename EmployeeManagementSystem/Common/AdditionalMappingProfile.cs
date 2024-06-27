using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Common
{
    public class AdditionalMappingProfile : Profile
    {
        public AdditionalMappingProfile()
        {
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
