using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.Interface
{
    public interface IEmployeeAdditionalDetailsService
    {
        Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto);
        Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetails();
        Task<EmployeeAdditionalDetailsDto> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto);
        Task<string> DeleteEmployeeAdditionalDetails(string uid);
        Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetailsByMakepostRequest(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto);
        Task<AdditionalEmployeeFilterCriteria> GetAllEmployeesAdditionalByPagination(AdditionalEmployeeFilterCriteria employeeFilterCriteria);
        //Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetailsByRole(string role);
        Task<EmployeeAdditionalDetailsDto> GetEmployeeAdditionalDetailsByBasicDetailsUId(string uid);
    }
}
