using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IManagerService
    {
        Task<ManagerDto> AddManager(ManagerDto managerDto);
        //Task<ManagerDto> GetManagerByUId(string uid);
        Task<ManagerDto> UpdateManager(ManagerDto managerDto);
        Task<string> DeleteManager(string uid);
    }
}
