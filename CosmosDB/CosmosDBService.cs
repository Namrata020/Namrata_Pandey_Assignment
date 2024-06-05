using Microsoft.Azure.Cosmos;
using System.Net;
using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.Entities;

namespace VisitorSecurityClearanceSystem.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosEndpoint, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.databaseName, Credentials.containerName);
        }

        //Create Visitor
        public async Task<VisitorEntity> AddVisitor(VisitorEntity visitor)
        {
            var response = await _container.CreateItemAsync(visitor);
            return response;
        }

        //Read Visitor
        public async Task<List<VisitorEntity>> GetAllVisitors()
        {
            var response = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(a => a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).ToList();
            return response;
        }

        //Find visitor by uid
        public async Task<VisitorEntity> GetVisitorByUId(string uId)
        {
            var visitor = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return visitor;
        }

        //Find visitor by email
        public async Task<VisitorEntity> GetVisitorByEmail(string email)
        {
            var visitor = _container.GetItemLinqQueryable<VisitorEntity>(true).Where(a => a.Email == email && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return visitor;
        }

        //Update Visitor
        public async Task<VisitorEntity> UpdateVisitor(VisitorEntity visitor)
        {
            var response = await _container.UpsertItemAsync(visitor, new PartitionKey(visitor.DocumentType));
            return response;
        }

        //Delete Visitor
        public async Task<string> DeleteVisitor(string uId)
        {
            var response = await _container.DeleteItemAsync<VisitorEntity>(uId, new PartitionKey(uId));
            return "Successfully Deleted!!";
        }

        public async Task<T> ReplaceAsync<T>(T item) where T : class
        {
            var response = await _container.ReplaceItemAsync<T>(item, (item as dynamic).Id, new PartitionKey((item as dynamic).DocumentType));
            return response;
        }



        //SECURITY 

        //Create Security
        public async Task<SecurityEntity> AddSecurity(SecurityEntity security)
        {
            var response = await _container.CreateItemAsync(security);
            return response;
        }

        //Read Security
        public async Task<List<SecurityEntity>> GetAllSecurities()
        {
            var response = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(a => a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).ToList();
            return response;
        }

        //Find Security by uid
        public async Task<SecurityEntity> GetSecurityByUId(string uId)
        {
            var security = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return security;
        }

        //Find Security by email
        public async Task<SecurityEntity> GetSecurityByEmail(string email)
        {
            var security = _container.GetItemLinqQueryable<SecurityEntity>(true).Where(a => a.Email == email && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return security;
        }

        //Update Security
        public async Task<SecurityEntity> UpdateSecurity(SecurityEntity security)
        {
            var response = await _container.UpsertItemAsync(security, new PartitionKey(security.DocumentType));
            return response;
        }

        //Delete Security
        public async Task<string> DeleteSecurity(string uId)
        {
            var response = await _container.DeleteItemAsync<SecurityEntity>(uId, new PartitionKey(uId));
            return "Successfully Deleted!!";
        }


        //Add Manager
        public async Task<ManagerEntity> AddManager(ManagerEntity manager)
        {
            var response = await _container.CreateItemAsync(manager);
            return response;
        }

        //Update manager
        public async Task<ManagerEntity> UpdateManager(ManagerEntity manager)
        {
            var response = await _container.UpsertItemAsync(manager, new PartitionKey(manager.DocumentType));
            return response;
        }

        //Delete manager
        public async Task<string> DeleteManager(string uId)
        {
            var response = await _container.DeleteItemAsync<ManagerEntity>(uId, new PartitionKey(uId));
            return "Successfully Deleted!!";
        }

        //Find manager by uid
        public async Task<ManagerEntity> GetManagerByUId(string uId)
        {
            var manager = _container.GetItemLinqQueryable<ManagerEntity>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return manager;
        }


        //Add office
        public async Task<OfficeEntity> AddOffice(OfficeEntity office)
        {
            var response = await _container.CreateItemAsync(office);
            return response;
        }

        //Find office by uid
        public async Task<OfficeEntity> GetOfficeByUId(string uId)
        {
            var office = _container.GetItemLinqQueryable<OfficeEntity>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.VisitorDocumentType).FirstOrDefault();
            return office;
        }

        //Update office
        public async Task<OfficeEntity> UpdateOffice(OfficeEntity office)
        {
            var response = await _container.UpsertItemAsync(office, new PartitionKey(office.DocumentType));
            return response;
        }

        //Delete office
        public async Task<string> DeleteOffice(string uId)
        {
            var response = await _container.DeleteItemAsync<ManagerEntity>(uId, new PartitionKey(uId));
            return "Successfully Deleted!!";
        }
    }
}
