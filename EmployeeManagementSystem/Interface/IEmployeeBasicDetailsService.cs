using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using static EmployeeManagementSystem.Entities.EmployeeBasicDetails;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeBasicDetailsService
    {
        Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto);
        Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto);
        Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetails();
        Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetails();
        Task<EmployeeBasicDetailsDto> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicdetailsDto);
        Task<string> DeleteEmployeeBasicDetails(string uid);
        Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetailsByMakepostRequest(EmployeeBasicDetailsDto employeeBasicDetailsDto);
        Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria);
        Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetailsByRole(string role);
    }
}
