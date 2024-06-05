using Microsoft.AspNetCore.Mvc;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VisitorController : Controller
    {
        public readonly IVisitorService _visitorService;
        public VisitorController(IVisitorService visitorService)
        {
            _visitorService = visitorService;
        }

        [HttpPost]
        public async Task<VisitorDto> AddVisitor(VisitorDto visitorDto)
        {
            var response = await _visitorService.AddVisitor(visitorDto);
            return response;
        }


        [HttpGet]
        public async Task<List<VisitorDto>> GetAllVisitors()
        {
            var response = await _visitorService.GetAllVisitors();
            return response;
        }

        [HttpPut]
        public async Task<VisitorDto> UpdateVisitor(VisitorDto visitorDto)
        {
            var response = await _visitorService.UpdateVisitor(visitorDto);
            return response;
        }

        [HttpDelete]
        public async Task<string> DeleteVisitor(string uid)
        {
            var response = await _visitorService.DeleteVisitor(uid);
            return response;
        }

    }
}
