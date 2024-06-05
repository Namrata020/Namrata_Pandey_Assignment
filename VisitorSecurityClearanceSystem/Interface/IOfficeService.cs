using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IOfficeService
    {
        Task<OfficeDto> AddOffice(OfficeDto officeDto);
        //Task<OfficeDto> GetOfficeByUId(string uid);
        Task<OfficeDto> UpdateOffice(OfficeDto office);
        Task<string> DeleteOffice(string uid);
    }
}
