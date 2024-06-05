using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        public readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost]
        public async Task<SecurityDto> AddSecurity(SecurityDto securityDto)
        {
            var response = await _securityService.AddSecurity(securityDto);
            return response;
        }


        [HttpPut]
        public async Task<SecurityDto> UpdateSecurity(SecurityDto securityDto)
        {
            var response = await _securityService.UpdateSecurity(securityDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteSecurity(string uid)
        {
            var response = await _securityService.DeleteSecurity(uid);
            return response;
        }
    }
}
