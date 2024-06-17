using AutoMapper;
using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
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


        public async Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var employeeAdditional = _mapper.Map<EmployeeAdditionalDetails>(employeeAdditionalDetailsDto);

            /*employeeAdditional.Id = Guid.NewGuid().ToString();
            employeeAdditional.UId = employeeAdditional.Id;
            employeeAdditional.DocumentType = Credentials.EmployeeDocumentType;
            employeeAdditional.CreatedBy = "User";
            employeeAdditional.CreatedOn = DateTime.Now;
            employeeAdditional.UpdatedBy = "";
            employeeAdditional.UpdatedOn = DateTime.Now;
            employeeAdditional.Version = 1;
            employeeAdditional.Active = true;
            employeeAdditional.Archived = false;*/

            var response = await _cosmosDBService.AddEmployeeAdditionalDetails(employeeAdditional);

            var responseDto = _mapper.Map<EmployeeAdditionalDetailsDto>(response);
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

            /*existingEmployee.Id = Guid.NewGuid().ToString();
            existingEmployee.UId = existingEmployee.Id;
            existingEmployee.DocumentType = Credentials.EmployeeDocumentType;
            existingEmployee.CreatedBy = "Admin";  
            existingEmployee.CreatedOn = DateTime.Now;
            existingEmployee.UpdatedBy = "";
            existingEmployee.UpdatedOn = DateTime.Now;
            existingEmployee.Version = 1;
            existingEmployee.Active = false;
            existingEmployee.Archived = true;*/

            //await _cosmosDBService.AddEmployeeBasicDetails(existingEmployee);

            return "Employee's Basic Details Deleted!!";
        }

        public async Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetails()
        {
            var employeeBasicDetailsList = await _cosmosDBService.GetAllEmployeeBasicDetails();
            var employeeBasicDetailsDtoList = _mapper.Map<List<EmployeeBasicDetailsDto>>(employeeBasicDetailsList);
            return employeeBasicDetailsDtoList;

        }

        public async Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetails()
        {
            var employeeAdditionalDetailsList = await _cosmosDBService.GetAllEmployeeAdditionalDetails();
            var employeeAdditionalDetailsDtoList = _mapper.Map<List<EmployeeAdditionalDetailsDto>>(employeeAdditionalDetailsList);
            return employeeAdditionalDetailsDtoList;
        }

        public async Task<EmployeeBasicDetailsDto> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicdetailsDto)
        {
            var existingEmployee = await _cosmosDBService.GetEmployeeBasicDetailsByUId(employeeBasicdetailsDto.UId);
            if (existingEmployee == null)
            {
                throw new Exception("Employee not found!");
            }

            existingEmployee.Active = false;
            existingEmployee.Archived = true;

            var updatedEmployeeBasic = _mapper.Map(employeeBasicdetailsDto, existingEmployee);

            updatedEmployeeBasic.UpdatedBy = "User";
            updatedEmployeeBasic.UpdatedOn = DateTime.Now;

            var response = await _cosmosDBService.UpdateEmployeeBasicDetails(updatedEmployeeBasic);

            var responseDto = _mapper.Map<EmployeeBasicDetailsDto>(response);
            return responseDto;
        }


        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            EmployeeFilterCriteria responseObject = new EmployeeFilterCriteria
            {
                page = employeeFilterCriteria.page,
                pageSize = employeeFilterCriteria.pageSize
            };

            var employees = await GetAllEmployeeBasicDetails();
            responseObject.TotalCount = employees.Count;
           /* responseObject.page = employeeFilterCriteria.page;
            responseObject.pageSize = employeeFilterCriteria.pageSize;
*/
            var skip = employeeFilterCriteria.pageSize * (employeeFilterCriteria.page - 1);

            responseObject.Employees = employees.Skip(skip).Take(employeeFilterCriteria.pageSize).ToList();
            return responseObject;
        }
    }
}
