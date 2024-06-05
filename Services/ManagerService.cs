using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entities;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class ManagerService : IManagerService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public ManagerService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<ManagerDto> AddManager(ManagerDto managerDto)
        {
            var existingManager = await _cosmosDBService.GetSecurityByUId(managerDto.UId);
            if (existingManager != null)
            {
                throw new InvalidOperationException("A visitor with this email already exists.");
            }


            ManagerEntity manager = new ManagerEntity();
            manager.Name = managerDto.Name;
            manager.Email = managerDto.Email;
            manager.PhoneNumber = managerDto.PhoneNumber;
            manager.Address = managerDto.Address;
            manager.CompanyName = managerDto.CompanyName;
            manager.Purpose = managerDto.Purpose;
            manager.Status = managerDto.Status;
            manager.EntryTime = managerDto.EntryTime;
            manager.ExitTime = managerDto.ExitTime;

            //Assign values to manadatory fields
            manager.Id = Guid.NewGuid().ToString();
            manager.UId = manager.Id;
            manager.DocumentType = Credentials.VisitorDocumentType;
            manager.CreatedBy = "ABC";
            manager.CreatedOn = DateTime.Now;
            manager.UpdatedBy = "";
            manager.UpdatedOn = DateTime.Now;
            manager.Version = 1;
            manager.Active = true;
            manager.Archived = false;

            var response = await _cosmosDBService.AddManager(manager);

            var responseModel = new ManagerDto();

            responseModel.UId = response.UId;
            responseModel.Name = response.Name;
            responseModel.Email = response.Email;
            responseModel.PhoneNumber = response.PhoneNumber;
            responseModel.Address = response.Address;
            responseModel.CompanyName = response.CompanyName;
            responseModel.Purpose = response.Purpose;
            responseModel.Status = response.Status;
            responseModel.EntryTime = response.EntryTime;
            responseModel.ExitTime = response.ExitTime;

            return responseModel;
        }

        public async Task<string> DeleteManager(string uid)
        {
            var manager = await _cosmosDBService.GetManagerByUId(uid);
            manager.Active = false;
            manager.Archived = true;
            await _cosmosDBService.ReplaceAsync(manager);

            //Assign values to manadatory fields
            manager.Id = Guid.NewGuid().ToString();
            manager.UId = manager.Id;
            manager.DocumentType = Credentials.VisitorDocumentType;
            manager.CreatedBy = "ABC";
            manager.CreatedOn = DateTime.Now;
            manager.UpdatedBy = "";
            manager.UpdatedOn = DateTime.Now;
            manager.Version = 1;
            manager.Active = false;
            manager.Archived = true;

            return (manager.Name + " is deleted successfully!!");
        }

        public async Task<ManagerDto> UpdateManager(ManagerDto managerDto)
        {
            var existingManager = await _cosmosDBService.GetVisitorByUId(managerDto.UId);

            // If the visitor is not found, throw an exception
            if (existingManager == null)
            {
                throw new InvalidOperationException("Visitor not found.");
            }

            // Update the existing visitor's properties with the new values
            existingManager.Name = managerDto.Name;
            existingManager.Email = managerDto.Email;
            existingManager.PhoneNumber = managerDto.PhoneNumber;
            existingManager.Address = managerDto.Address;
            existingManager.CompanyName = managerDto.CompanyName;
            existingManager.Purpose = managerDto.Purpose;
            existingManager.Status = managerDto.Status;
            existingManager.EntryTime = managerDto.EntryTime;
            existingManager.ExitTime = managerDto.ExitTime;
            existingManager.UpdatedBy = "ABC"; // Update this value as needed
            existingManager.UpdatedOn = DateTime.Now;

            // Update the visitor in the database
            var updatedVisitor = await _cosmosDBService.UpdateVisitor(existingManager);

            // Create a response model with the updated visitor's details
            var responseModel = new ManagerDto
            {
                UId = updatedVisitor.UId,
                Name = updatedVisitor.Name,
                Email = updatedVisitor.Email,
                PhoneNumber = updatedVisitor.PhoneNumber,
                Address = updatedVisitor.Address,
                CompanyName = updatedVisitor.CompanyName,
                Purpose = updatedVisitor.Purpose,
                Status = updatedVisitor.Status,
                EntryTime = updatedVisitor.EntryTime,
                ExitTime = updatedVisitor.ExitTime
            };

            return responseModel;
        }
    }
}
