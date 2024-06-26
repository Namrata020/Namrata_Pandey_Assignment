using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Service
{
    public class EmployeeAdditionalDetailsService : IEmployeeAdditionalDetailsService
    {
        public readonly ICosmosDBService _cosmosDBService;
        private readonly IMapper _mapper;

        public EmployeeAdditionalDetailsService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var employeeAdditional = _mapper.Map<EmployeeAdditionalDetails>(employeeAdditionalDetailsDto);

            var response = await _cosmosDBService.AddEmployeeAdditionalDetails(employeeAdditional);
            var responseDto = _mapper.Map<EmployeeAdditionalDetailsDto>(response);

            return responseDto;
        }

        public async Task<string> DeleteEmployeeAdditionalDetails(string uid)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(uid);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found!");
            }

            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            existingEmployee.UpdatedBy = "Admin";
            existingEmployee.UpdatedOn = DateTime.UtcNow;

            await _cosmosDBService.ReplaceAsync(existingEmployee);

            return "Employee's Additional Details Deleted!!";
        }

        public async Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetails()
        {
            var employeeAdditionalDetailsList = await _cosmosDBService.GetAllEmployeeAdditionalDetails();
            var employeeAdditionalDetailsDtoList = _mapper.Map<List<EmployeeAdditionalDetailsDto>>(employeeAdditionalDetailsList);
            return employeeAdditionalDetailsDtoList;
        }

        public async Task<EmployeeAdditionalDetailsDto> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(employeeAdditionalDetailsDto.UId);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found!");
            }

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            var updatedEmployeeAdditional = _mapper.Map(employeeAdditionalDetailsDto, existingEmployee);

            updatedEmployeeAdditional.UpdatedBy = "User";
            updatedEmployeeAdditional.UpdatedOn = DateTime.Now;

            var response = await _cosmosDBService.UpdateEmployeeAdditionalDetails(updatedEmployeeAdditional);

            var responseDto = _mapper.Map<EmployeeAdditionalDetailsDto>(response);
            return responseDto;
        }

        public async Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetailsByMakepostRequest(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var serializedObj = JsonConvert.SerializeObject(employeeAdditionalDetailsDto);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.VisitorUrl, Credentials.AddEmployeeEndpoint, serializedObj);
            var responseObj = JsonConvert.DeserializeObject<EmployeeAdditionalDetailsDto>(requestObj);
            return responseObj;
        }

        public async Task<AdditionalEmployeeFilterCriteria> GetAllEmployeesAdditionalByPagination(AdditionalEmployeeFilterCriteria employeeFilterCriteria)
        {
            AdditionalEmployeeFilterCriteria responseObject = new AdditionalEmployeeFilterCriteria();
            var checkFilter = employeeFilterCriteria.Filters.Any(e => e.FieldName == "status");
            var status = "";

            if (checkFilter)
            {
                status = employeeFilterCriteria.Filters.Find(e => e.FieldName == "status").FieldValue;
            }

            var employees = await GetAllEmployeeAdditionalDetails();

            var filteredRecords = employees.FindAll(e => e.Status == status);

            responseObject.TotalCount = employees.Count;
            responseObject.Page = employeeFilterCriteria.Page;
            responseObject.PageSize = employeeFilterCriteria.PageSize;

            var skip = employeeFilterCriteria.PageSize * (employeeFilterCriteria.Page - 1);

            filteredRecords = filteredRecords.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();

            //employees = employees.Skip(skip).Take(employeeFilterCriteria.pageSize).ToList();
            foreach (var item in filteredRecords)
            {
                responseObject.AdditionalEmployees.Add(item);
            }

            return responseObject;
        }

        public async Task<EmployeeAdditionalDetailsDto> GetEmployeeAdditionalDetailsByBasicDetailsUId(string uid)
        {
            var basicDetails = await _cosmosDBService.GetEmployeeBasicDetailsByUId(uid);
            if(basicDetails == null)
            {
                throw new Exception("Employee basic details not found!!");
            }

            var additionalDetails = await _cosmosDBService.GetEmployeeAdditionalDetailsByUId(uid);
            if(additionalDetails == null)
            {
                throw new Exception("Employee additional details not found!!");
            }

            var additionalDetailsDto = _mapper.Map<EmployeeAdditionalDetailsDto>(additionalDetails);
            return additionalDetailsDto;
        }

        /* public async Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetailsByRole(string role)
         {
             var allEmployees = await GetAllEmployeeAdditionalDetails();
             return allEmployees.FindAll(e => e.Role == role);
         }*/


    }
}
