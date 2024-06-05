using VisitorSecurityClearanceSystem.DTO;

namespace VisitorSecurityClearanceSystem.Interface
{
    public interface IVisitorService
    {
        Task<VisitorDto> AddVisitor(VisitorDto visitorDto);
        Task<List<VisitorDto>> GetAllVisitors();
        Task<VisitorDto> UpdateVisitor(VisitorDto visitor);
        Task<string> DeleteVisitor(string uid);
    }
}
