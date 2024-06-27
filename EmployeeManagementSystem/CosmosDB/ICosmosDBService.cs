using EmployeeManagementSystem.Entities;

namespace EmployeeManagementSystem.CosmosDB
{
    public interface ICosmosDBService
    {
        //EMPLOYEE BASIC DETAILS
        Task<EmployeeBasicDetails> AddEmployeeBasicDetails(EmployeeBasicDetails employeeBasicDetails);
        Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails();
        Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByUId(string uId);
        Task<EmployeeBasicDetails> UpdateEmployeeBasicDetails(EmployeeBasicDetails updatedEmployeeBasicDetails);
        Task<EmployeeBasicDetails> ReplaceAsync(EmployeeBasicDetails employeeBasicDetails);
        Task<string> DeleteEmployeeBasicDetails(string uId);
        

        //EMPLOYEE ADDITIONAL DETAILS
        Task<EmployeeAdditionalDetails> AddEmployeeAdditionalDetails(EmployeeAdditionalDetails employeeAdditionalDetails);
        Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails();
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByUId(string uId);
        Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetails updatedEmployeeAdditionalDetails);
        Task<EmployeeAdditionalDetails> ReplaceAsync(EmployeeAdditionalDetails employeeAdditionalDetails);
        Task<string> DeleteEmployeeAdditionalDetails(string uId);
        Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByBasicDetailsUId(string uId);

    }
}
