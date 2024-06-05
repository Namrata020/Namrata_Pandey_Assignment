using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;
using VisitorSecurityClearanceSystem.Services;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : Controller
    {
        public readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost]
        public async Task<ManagerDto> AddSecurity(ManagerDto managerDto)
        {
            var response = await _managerService.AddManager(managerDto);
            return response;
        }


        [HttpPut]
        public async Task<ManagerDto> UpdateSecurity(ManagerDto managerDto)
        {
            var response = await _managerService.UpdateManager(managerDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteSecurity(string uid)
        {
            var response = await _managerService.DeleteManager(uid);
            return response;
        }

    }
}
