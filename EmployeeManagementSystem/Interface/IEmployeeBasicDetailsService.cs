using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using static EmployeeManagementSystem.Entities.EmployeeBasicDetails;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeBasicDetailsService
    {
        Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto);
        Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetails();
        Task<EmployeeBasicDetailsDto> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto);
        Task<string> DeleteEmployeeBasicDetails(string uid);
        Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetailsByMakepostRequest(EmployeeBasicDetailsDto employeeBasicDetailsDto);
        Task<BasicEmployeeFilterCriteria> GetAllEmployeesBasicByPagination(BasicEmployeeFilterCriteria employeeFilterCriteria);
        Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetailsByRole(string role);
    }
}
