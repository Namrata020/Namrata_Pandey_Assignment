using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.ServiceFilter;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;


namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeBasicDetailsController : Controller
    {
        public readonly IEmployeeBasicDetailsService _employeeBasicDetailsService;
        public readonly IEmployeeAdditionalDetailsService _employeeAdditionalDetailsService;
        public EmployeeBasicDetailsController(IEmployeeBasicDetailsService employeeBasicDetailsService, IEmployeeAdditionalDetailsService employeeAdditionalDetailsService)
        {
            _employeeBasicDetailsService = employeeBasicDetailsService;
            _employeeAdditionalDetailsService = employeeAdditionalDetailsService;
        }

        [HttpPost]
        public async Task<EmployeeBasicDetailsDto> AddEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var response = await _employeeBasicDetailsService.AddEmployeeBasicDetails(employeeBasicDetailsDto);
            return response;
        }

        [HttpGet]
        public async Task<List<EmployeeBasicDetailsDto>> GetAllEmployeeBasicDetails()
        {
            var response = await _employeeBasicDetailsService.GetAllEmployeeBasicDetails();
            return response;
        }

        [HttpPut]
        public async Task<EmployeeBasicDetailsDto> UpdateEmployeeBasicDetails(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var response = await _employeeBasicDetailsService.UpdateEmployeeBasicDetails(employeeBasicDetailsDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteEmployeeBasicDetails(string uId)
        {
            var response = await _employeeBasicDetailsService.DeleteEmployeeBasicDetails(uId);
            return response;
        }

        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or null");

            var basicDetailsList = new List<EmployeeBasicDetailsDto>();
            var additionalDetailsList = new List<EmployeeAdditionalDetailsDto>();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var employee = new EmployeeBasicDetailsDto
                        {
                            UId = Guid.NewGuid().ToString(),
                            FirstName = GetStringFromCell(worksheet, row, 2),
                            LastName = GetStringFromCell(worksheet, row, 3),
                            Email = GetStringFromCell(worksheet, row, 4),
                            Mobile = GetStringFromCell(worksheet, row, 5),
                            ReportingManagerName = GetStringFromCell(worksheet, row, 6),
                            //DateOfBirth = GetStringFromCell(worksheet, row, 7),
                            //DateOfJoining = GetStringFromCell(worksheet, row, 8)
                            Address = new Address(),
                            Status = "Active"
                        };

                        var additionalDetails = new EmployeeAdditionalDetailsDto
                        {
                            EmployeeBasicDetailsUId = employee.UId,
                            WorkInformation = new WorkInfo_
                            {
                                DateOfJoining = DateTime.Parse(GetStringFromCell(worksheet, row, 8))
                            },
                            PersonalDetails = new PersonalDetails_
                            {
                                DateOfBirth = DateTime.Parse(GetStringFromCell(worksheet,row,7))
                            }
                            
                        };

                        /*await AddEmployeeBasicDetails(employee);
                        await _employeeAdditionalDetailsService.AddEmployeeAdditionalDetails(additionalDetails);
*/
                        var addedEmployee = await _employeeBasicDetailsService.AddEmployeeBasicDetails(employee);
                        if (addedEmployee != null)
                        {
                            basicDetailsList.Add(employee);
                            var addedAdditionalDetails = await _employeeAdditionalDetailsService.AddEmployeeAdditionalDetails(additionalDetails);
                            additionalDetailsList.Add(addedAdditionalDetails);
                        }

                        //employees.Add(employee);
                    }
                }
            }
            return Ok(new
            {
                Employees = basicDetailsList,
                AdditionalDetails = additionalDetailsList
            });

        }


        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var basicDetails = await _employeeBasicDetailsService.GetAllEmployeeBasicDetails();
            var additionalDetails = await _employeeAdditionalDetailsService.GetAllEmployeeAdditionalDetails();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("basicDetails");

                //Add header
                worksheet.Cells[1, 1].Value = "Sr.No.";
                worksheet.Cells[1, 2].Value = "Salutory";
                worksheet.Cells[1, 3].Value = "First Name";
                worksheet.Cells[1, 4].Value = "Middle Name";
                worksheet.Cells[1, 5].Value = "Last Name";
                worksheet.Cells[1, 6].Value = "Nick Name";
                worksheet.Cells[1, 7].Value = "Email";
                worksheet.Cells[1, 8].Value = "Phone No.";
                worksheet.Cells[1, 9].Value = "Employee ID";
                worksheet.Cells[1, 10].Value = "Role";
                worksheet.Cells[1, 11].Value = "Reporting Manager UId";
                worksheet.Cells[1, 12].Value = "Reporting Manager Name";
                worksheet.Cells[1, 13].Value = "Address";
                worksheet.Cells[1, 14].Value = "Basic Status";
                worksheet.Cells[1, 15].Value = "Employee Basic Details UId";
                worksheet.Cells[1, 16].Value = "Alternate Email";
                worksheet.Cells[1, 17].Value = "Alternate Mobile";
                worksheet.Cells[1, 18].Value = "Work Information";
                worksheet.Cells[1, 19].Value = "Personal Details";
                worksheet.Cells[1, 20].Value = "Identity Information";
                worksheet.Cells[1, 21].Value = "Additional Status";

                //header style
                using (var range = worksheet.Cells[1, 1, 1, 21])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Purple);
                    range.Style.Font.Color.SetColor(Color.Black);
                }

                //Add data
                for (int i = 0; i < basicDetails.Count; i++)
                {
                    var employee = basicDetails[i];
                    var additionalDetail = additionalDetails.FirstOrDefault(ad => ad.EmployeeBasicDetailsUId == employee.UId);

                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = employee.Salutory;
                    worksheet.Cells[i + 2, 3].Value = employee.FirstName;
                    worksheet.Cells[i + 2, 4].Value = employee.MiddleName;
                    worksheet.Cells[i + 2, 5].Value = employee.LastName;
                    worksheet.Cells[i + 2, 6].Value = employee.NickName;
                    worksheet.Cells[i + 2, 7].Value = employee.Email;
                    worksheet.Cells[i + 2, 8].Value = employee.Mobile;
                    worksheet.Cells[i + 2, 9].Value = employee.EmployeeID;
                    worksheet.Cells[i + 2, 10].Value = employee.Role;
                    worksheet.Cells[i + 2, 11].Value = employee.ReportingManagerUId;
                    worksheet.Cells[i + 2, 12].Value = employee.ReportingManagerName;
                    worksheet.Cells[i + 2, 13].Value = employee.Address;
                    worksheet.Cells[i + 2, 14].Value = employee.Status;

                    if(additionalDetail != null)
                    {
                        worksheet.Cells[i + 2, 15].Value = additionalDetail.EmployeeBasicDetailsUId;
                        worksheet.Cells[i + 2, 16].Value = additionalDetail.AlternateEmail;
                        worksheet.Cells[i + 2, 17].Value = additionalDetail.AlternateMobile;
                        worksheet.Cells[i + 2, 18].Value = additionalDetail.WorkInformation;
                        worksheet.Cells[i + 2, 19].Value = additionalDetail.PersonalDetails;
                        worksheet.Cells[i + 2, 20].Value = additionalDetail.IdentityInformation;
                        worksheet.Cells[i + 2, 21].Value = additionalDetail.Status;
                    }
                }
            

                var stream = new System.IO.MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var fileName = "EmployeeDetails.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeBasicDetailsByRole(string role)
        {
            var response = await _employeeBasicDetailsService.GetAllEmployeeBasicDetailsByRole(role);
            return Ok(response);
        }

        [HttpPost]
        [ServiceFilter(typeof(BuildEmployeeFilter))]
        public async Task<BasicEmployeeFilterCriteria> GetAllEmployeesBasicByPagination(BasicEmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await _employeeBasicDetailsService.GetAllEmployeesBasicByPagination(employeeFilterCriteria);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeBasicDetailsByMakePostRequest(EmployeeBasicDetailsDto employeeBasicDetailsDto)
        {
            var response = await _employeeBasicDetailsService.AddEmployeeBasicDetailsByMakepostRequest(employeeBasicDetailsDto);
            return Ok(response);
        }

        
    }

}
