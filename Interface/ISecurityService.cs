using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface ISecurityService
    {
        Task<SecurityDto> AddSecurity(SecurityDto securityDto);
        //Task<SecurityDto> GetSecuritieByUId(string uid);
        Task<SecurityDto> UpdateSecurity(SecurityDto security);
        Task<string> DeleteSecurity(string uid);
    }
}
