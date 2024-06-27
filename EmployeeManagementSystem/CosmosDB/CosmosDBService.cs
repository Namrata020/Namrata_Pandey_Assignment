using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.CosmosDB
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



        //BASIC DETAILS CRUD OPERATIONS

        public async Task<EmployeeBasicDetails> AddEmployeeBasicDetails(EmployeeBasicDetails employeeBasicDetails)
        {
           var response = await _container.CreateItemAsync(employeeBasicDetails, new PartitionKey(employeeBasicDetails.DocumentType));
            return response;
        }

        public async Task<string> DeleteEmployeeBasicDetails(string uId)
        {
            var employee = await GetEmployeeBasicDetailsByUId(uId);
            if (employee != null)
            {
                await _container.DeleteItemAsync<EmployeeBasicDetails>(employee.Id, new PartitionKey(employee.DocumentType));
                return "Successfully Deleted!";
            }

            return "Employee not found!";

        }

        public async Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails()
        {
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.Active == true && a.Archived == false && a.DocumentType == Credentials.EmployeeDocumentType).ToFeedIterator();
            var response = new List<EmployeeBasicDetails>();

            while (query.HasMoreResults)
            {
                var item = await query.ReadNextAsync();
                response.AddRange(item);
            }
            
            return response;     
        }

        public async Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByUId(string uId)
        {
            var query = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.EmployeeDocumentType).ToFeedIterator();
            var response = await query.ReadNextAsync();

            return response.FirstOrDefault();
        }

        public async Task<EmployeeBasicDetails> UpdateEmployeeBasicDetails(EmployeeBasicDetails updatedEmployeeBasicDetails)
        {
            var response = await _container.UpsertItemAsync(updatedEmployeeBasicDetails, new PartitionKey(updatedEmployeeBasicDetails.DocumentType));
            return response;
        }

        public async Task<EmployeeBasicDetails> ReplaceAsync(EmployeeBasicDetails employeeBasicDetails)
        {
            var response = await _container.ReplaceItemAsync(employeeBasicDetails, employeeBasicDetails.Id, new PartitionKey(employeeBasicDetails.DocumentType));
            return response;
        }


        //ADDITIONAL DETAILS CRUD OPERATIONS

        public async Task<EmployeeAdditionalDetails> AddEmployeeAdditionalDetails(EmployeeAdditionalDetails employeeAdditionalDetails)
        {
            var response = await _container.CreateItemAsync(employeeAdditionalDetails, new PartitionKey(employeeAdditionalDetails.DocumentType));
            return response;
        }


        public async Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails()
        {
            var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(a => a.Active == true && a.Archived == false && a.DocumentType == Credentials.EmployeeDocumentType).ToFeedIterator();
            var response = new List<EmployeeAdditionalDetails>();

            while (query.HasMoreResults)
            {
                var item = await query.ReadNextAsync();
                response.AddRange(item);
            }

            return response;
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByUId(string uId)
        {
            var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(a => a.UId == uId && a.Active == true && a.Archived == false && a.DocumentType == Credentials.EmployeeDocumentType).ToFeedIterator();
            var response = await query.ReadNextAsync();

            return response.FirstOrDefault();
        }

        public async Task<EmployeeAdditionalDetails> UpdateEmployeeAdditionalDetails(EmployeeAdditionalDetails updatedEmployeeAdditionalDetails)
        {
            var response = await _container.UpsertItemAsync(updatedEmployeeAdditionalDetails, new PartitionKey(updatedEmployeeAdditionalDetails.DocumentType));
            return response;
        }

        public async Task<EmployeeAdditionalDetails> ReplaceAsync(EmployeeAdditionalDetails employeeAdditionalDetails)
        {
            var response = await _container.ReplaceItemAsync(employeeAdditionalDetails, employeeAdditionalDetails.Id, new PartitionKey(employeeAdditionalDetails.DocumentType));
            return response;
        }

        public async Task<string> DeleteEmployeeAdditionalDetails(string uId)
        {
            var employee = await GetEmployeeAdditionalDetailsByUId(uId);
            if (employee != null)
            {
                await _container.DeleteItemAsync<EmployeeAdditionalDetails>(employee.Id, new PartitionKey(employee.DocumentType));
                return "Successfully Deleted!";
            }

            return "Employee not found!";
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByBasicDetailsUId(string uId)
        {
            var query = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(e => e.EmployeeBasicDetailsUId == uId && e.Active == true && e.Archived == false && e.DocumentType == Credentials.EmployeeDocumentType).ToFeedIterator();
            var response = new List<EmployeeAdditionalDetails>();

            while(await query.ReadNextAsync() is var item && item != null)
            {
                response.AddRange(item);
            }

            return response.FirstOrDefault();
        }
    }
}
