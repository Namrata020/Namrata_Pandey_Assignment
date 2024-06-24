using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entities;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.ServiceFilter;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        public EmployeeBasicDetailsController(IEmployeeBasicDetailsService employeeBasicDetailsService)
        {
            _employeeBasicDetailsService = employeeBasicDetailsService;
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

            var employees = new List<EmployeeBasicDetailsDto>();
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
                            UId = GetStringFromCell(worksheet, row, 1),
                            FirstName = GetStringFromCell(worksheet, row, 2),
                            LastName = GetStringFromCell(worksheet, row, 3),
                            Email = GetStringFromCell(worksheet, row, 4),
                            Mobile = GetStringFromCell(worksheet, row, 5),
                            ReportingManagerName = GetStringFromCell(worksheet, row, 6),
                            //DateOfBirth = GetStringFromCell(worksheet, row, 7),
                            //DateOfJoining = GetStringFromCell(worksheet, row, 8)
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

                        await AddEmployeeBasicDetails(employee);
                        await _employeeBasicDetailsService.AddEmployeeAdditionalDetails(additionalDetails);

                        employees.Add(employee);
                    }
                }
            }
            return Ok((employees));

        }


        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var employees = await _employeeBasicDetailsService.GetAllEmployeeBasicDetails();
            var additionalDetails = await _employeeBasicDetailsService.GetAllEmployeeAdditionalDetails();

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("employees");

                //Add header
                worksheet.Cells[1, 1].Value = "Sr.No.";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Last Name";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Phone No.";
                worksheet.Cells[1, 6].Value = "Reporting Manager Name";
                worksheet.Cells[1, 7].Value = "Date of Birth";
                worksheet.Cells[1, 8].Value = "Date of Joining";

                //header style
                using (var range = worksheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.Pink);
                }

                //Add data
                for (int i = 0; i < employees.Count; i++)
                {
                    var employee = employees[i];
                    var additionalDetail = additionalDetails.FirstOrDefault(ad => ad.EmployeeBasicDetailsUId == employee.UId);

                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = employee.FirstName;
                    worksheet.Cells[i + 2, 3].Value = employee.LastName;
                    worksheet.Cells[i + 2, 4].Value = employee.Email;
                    worksheet.Cells[i + 2, 5].Value = employee.Mobile;
                    worksheet.Cells[i + 2, 6].Value = employee.ReportingManagerName;
                    worksheet.Cells[i + 2, 7].Value = additionalDetail?.PersonalDetails?.DateOfBirth.ToString("yyyy-MM-dd");
                    worksheet.Cells[i + 2, 8].Value = additionalDetail?.WorkInformation?.DateOfJoining.ToString("yyyy-MM-dd");
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
        public async Task<EmployeeFilterCriteria> GetAllEmployeesByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await _employeeBasicDetailsService.GetAllEmployeesByPagination(employeeFilterCriteria);
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
