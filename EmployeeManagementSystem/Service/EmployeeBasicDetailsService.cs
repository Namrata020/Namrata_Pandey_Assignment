using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using Newtonsoft.Json;
using static EmployeeManagementSystem.Entities.EmployeeBasicDetails;

namespace EmployeeManagementSystem.Service
{
    public class EmployeeBasicDetailsService : IEmployeeBasicDetailsService
    {
        public readonly ICosmosDBService _cosmosDBService;
        private readonly IMapper _mapper;

        public EmployeeBasicDetailsService(ICosmosDBService cosmosDBService, IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }

        

        public async Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeBasicDetailsByUId(employeeBasicDetailsDto.UId);
            if (existingEmployee != null) {
                throw new Exception("Employee with UID already exists!!!!");
            }

            var employeeBasic = _mapper.Map<EmployeeBasicDetails>(employeeBasicDetailsDto);

            employeeBasic.Id = Guid.NewGuid().ToString();
            employeeBasic.UId = employeeBasicDetailsDto.UId ?? employeeBasic.Id;
            employeeBasic.DocumentType = Credentials.EmployeeDocumentType;
            employeeBasic.CreatedBy = "Admin"; 
            employeeBasic.CreatedOn = DateTime.Now;
            employeeBasic.UpdatedBy = "";
            employeeBasic.UpdatedOn = DateTime.Now;
            employeeBasic.Version = 1;
            employeeBasic.Active = true;
            employeeBasic.Archived = false;

            var response = await _cosmosDBService.AddEmployeeBasicDetails(employeeBasic);

            var responseDto = _mapper.Map<EmployeeBasicDetailsDto>(response);
            return responseDto;

        }

        public async Task<string> DeleteEmployeeBasicDetails(string uid)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeBasicDetailsByUId(uid);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found!");
            }

            existingEmployee.Active = false;
            existingEmployee.Archived = true;
            existingEmployee.UpdatedBy = "Admin";
            existingEmployee.UpdatedOn = DateTime.UtcNow;

            await _cosmosDBService.ReplaceAsync(existingEmployee);

            return "Employee's Basic Details Deleted!!";
        }

        public async Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetails()
        {
            var employeeBasicDetailsList = await _cosmosDBService.GetAllEmployeeBasicDetails();
            var employeeBasicDetailsDtoList = _mapper.Map<List<EmployeeBasicDetailsDto>>(employeeBasicDetailsList);
            return employeeBasicDetailsDtoList;

        }

        public async Task<EmployeeBasicDetailsDto> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeBasicDetailsByUId(employeeBasicDetailsDto.UId);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found!");
            }

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            var updatedEmployeeBasic = _mapper.Map(employeeBasicDetailsDto, existingEmployee);

            updatedEmployeeBasic.UpdatedBy = "User";
            updatedEmployeeBasic.UpdatedOn = DateTime.Now;

            var response = await _cosmosDBService.UpdateEmployeeBasicDetails(updatedEmployeeBasic);

            var responseDto = _mapper.Map<EmployeeBasicDetailsDto>(response);
            return responseDto;
        }


        public async Task<BasicEmployeeFilterCriteria> GetAllEmployeesBasicByPagination(BasicEmployeeFilterCriteria employeeFilterCriteria)
        {
            BasicEmployeeFilterCriteria  responseObject = new BasicEmployeeFilterCriteria();
            var checkFilter = employeeFilterCriteria.Filters.Any(e => e.FieldName == "status");
            var status = "";

            if(checkFilter)
            {
                status = employeeFilterCriteria.Filters.Find(e => e.FieldName == "status").FieldValue;
            }

            var employees = await GetAllEmployeeBasicDetails();

            var filteredRecords = employees.FindAll(e => e.Status == status);

            responseObject.TotalCount = employees.Count;
            responseObject.Page = employeeFilterCriteria.Page;
            responseObject.PageSize = employeeFilterCriteria.PageSize;

            var skip = employeeFilterCriteria.PageSize * (employeeFilterCriteria.Page - 1);

            filteredRecords = filteredRecords.Skip(skip).Take(employeeFilterCriteria.PageSize).ToList();

            //employees = employees.Skip(skip).Take(employeeFilterCriteria.pageSize).ToList();
            foreach(var item in filteredRecords)
            {
                responseObject.BasicEmployees.Add(item);
            }

            return responseObject;
        }

        public async Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetailsByRole(string role)
        {
            var allEmployees = await GetAllEmployeeBasicDetails();
            return allEmployees.FindAll(e => e.Role == role);
        }

        public async Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetailsByMakepostRequest(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var serializedObj = JsonConvert.SerializeObject(employeeBasicDetailsDto);
            var requestObj = await HttpClientHelper.MakePostRequest(Credentials.VisitorUrl, Credentials.AddEmployeeEndpoint, serializedObj);
            var responseObj = JsonConvert.DeserializeObject<EmployeeBasicDetailsDto>(requestObj);
            return responseObj;

        }
    }
}
