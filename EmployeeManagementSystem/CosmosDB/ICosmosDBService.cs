using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.CosmosDB
{
    public interface ICosmosDBService
    {
        Task<EmployeeBasicDetails> AddEmployeeBasicDetails(EmployeeBasicDetails employeeBasicDetails);
        Task<EmployeeAdditionalDetails> AddEmployeeAdditionalDetails(EmployeeAdditionalDetails employeeAdditionalDetails);
        Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails();
        Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByUId(string uId);
        Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails();

        Task<EmployeeBasicDetails> UpdateEmployeeBasicDetails(EmployeeBasicDetails updatedEmployeeBasicDetails);
        Task<string> DeleteEmployeeBasicDetails(string uId);
        Task<EmployeeBasicDetails> ReplaceAsync(EmployeeBasicDetails employeeBasicDetails);
    }
}
