using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Service;
using EmployeeManagementSystem.ServiceFilter;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : Controller
    {
        public readonly IEmployeeAdditionalDetailsService _employeeAdditionalDetailsService;
        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetailsService employeeAdditionalDetailsService)
        {
            _employeeAdditionalDetailsService = employeeAdditionalDetailsService;
        }

        [HttpPost]
        public async Task<EmployeeAdditionalDetailsDto> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var response = await _employeeAdditionalDetailsService.AddEmployeeAdditionalDetails(employeeAdditionalDetailsDto);
            return response;
        }

        [HttpGet]
        public async Task<List<EmployeeAdditionalDetailsDto>> GetAllEmployeeAdditionalDetails()
        {
            var response = await _employeeAdditionalDetailsService.GetAllEmployeeAdditionalDetails();
            return response;
        }

        [HttpPut]
        public async Task<EmployeeAdditionalDetailsDto> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var response = await _employeeAdditionalDetailsService.UpdateEmployeeAdditionalDetails(employeeAdditionalDetailsDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteEmployeeAdditionalDetails(string uId)
        {
            var response = await _employeeAdditionalDetailsService.DeleteEmployeeAdditionalDetails(uId);
            return response;
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<AdditionalEmployeeFilterCriteria> GetAllEmployeesAdditionalByPagination(AdditionalEmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await _employeeAdditionalDetailsService.GetAllEmployeesAdditionalByPagination(employeeFilterCriteria);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAdditionalDetailsByMakePostRequest(EmployeeAdditionalDetailsDto employeeAdditionalDetailsDto)
        {
            var response = await _employeeAdditionalDetailsService.AddEmployeeAdditionalDetailsByMakepostRequest(employeeAdditionalDetailsDto);
            return Ok(response);
        }


    }
}
