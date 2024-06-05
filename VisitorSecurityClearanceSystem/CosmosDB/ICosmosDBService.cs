using VisitorSecurityClearanceSystem.Entities;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public interface ICosmosDBService
    {
        //Entity
        Task<VisitorEntity> AddVisitor(VisitorEntity visitor);
        Task<List<VisitorEntity>> GetAllVisitors();
        Task<VisitorEntity> GetVisitorByUId(string uId);
        Task<VisitorEntity> GetVisitorByEmail(string email);
        Task<VisitorEntity> UpdateVisitor(VisitorEntity visitor);
        Task<string> DeleteVisitor(string uId);

        //Common method for all entities
        Task<T> ReplaceAsync<T>(T item) where T : class;

        //Security 
        Task<SecurityEntity> AddSecurity(SecurityEntity security);
        Task<SecurityEntity> GetSecurityByUId(string uId);
        Task<SecurityEntity> UpdateSecurity(SecurityEntity security);
        Task<string> DeleteSecurity(string uId);

        //MANAGER 
        Task<ManagerEntity> AddManager(ManagerEntity manager);
        Task<ManagerEntity> GetManagerByUId(string uId);
        Task<ManagerEntity> UpdateManager(ManagerEntity manager);
        Task<string> DeleteManager(string uId);

        //OFFICE 
        Task<OfficeEntity> AddOffice(OfficeEntity office);
        Task<OfficeEntity> GetOfficeByUId(string uId);
        Task<OfficeEntity> UpdateOffice(OfficeEntity office);
        Task<string> DeleteOffice(string uId);
    }
}
