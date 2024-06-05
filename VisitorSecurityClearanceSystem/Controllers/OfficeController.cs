using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfficeController : Controller
    {
       
        public readonly IOfficeService _officeService;
        public OfficeController(IOfficeService officeService)
        {
            _officeService = officeService;
        }

        [HttpPost]
        public async Task<OfficeDto> AddSecurity(OfficeDto officeDto)
        {
            var response = await _officeService.AddOffice(officeDto);
            return response;
        }


        [HttpPut]
        public async Task<OfficeDto> UpdateSecurity(OfficeDto officeDto)
        {
            var response = await _officeService.UpdateOffice(officeDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteSecurity(string uid)
        {
            var response = await _officeService.DeleteOffice(uid);
            return response;
        }
    }
}
