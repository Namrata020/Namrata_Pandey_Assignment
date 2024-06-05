using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entities;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class VisitorService : IVisitorService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public VisitorService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<VisitorDto> AddVisitor(VisitorDto visitorDto)
        {

            // Check if a visitor with the same email already exists
            var existingVisitor = await _cosmosDBService.GetVisitorByEmail(visitorDto.Email);
            if (existingVisitor != null)
            {
                throw new InvalidOperationException("A visitor with this email already exists.");
            }


            VisitorEntity visitor =new VisitorEntity();
            visitor.Name = visitorDto.Name;
            visitor.Email = visitorDto.Email;
            visitor.PhoneNumber = visitorDto.PhoneNumber;
            visitor.Address = visitorDto.Address;
            visitor.CompanyName = visitorDto.CompanyName;
            visitor.Purpose = visitorDto.Purpose;
            visitor.Status = visitorDto.Status;
            visitor.EntryTime = visitorDto.EntryTime;
            visitor.ExitTime = visitorDto.ExitTime;

            //Assign values to manadatory fields
            visitor.Id = Guid.NewGuid().ToString();
            visitor.UId = visitor.Id;
            visitor.DocumentType = Credentials.VisitorDocumentType;
            visitor.CreatedBy = "ABC";
            visitor.CreatedOn = DateTime.Now;
            visitor.UpdatedBy = "";
            visitor.UpdatedOn = DateTime.Now;
            visitor.Version = 1;
            visitor.Active = true;
            visitor.Archived = false;

            var response = await _cosmosDBService.AddVisitor(visitor);

            var responseModel = new VisitorDto();

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

        public async Task<List<VisitorDto>> GetAllVisitors()
        {
            var visitors = await _cosmosDBService.GetAllVisitors();

            var visitorModels = new List<VisitorDto>();

            foreach (var visitor in visitors)
            {
                var visitorModel = new VisitorDto();
                visitorModel.UId = visitor.UId;
                visitorModel.Name = visitor.Name;
                visitorModel.Email = visitor.Email;
                visitorModel.PhoneNumber = visitor.PhoneNumber;
                visitorModel.Address = visitor.Address;
                visitorModel.CompanyName = visitor.CompanyName;
                visitorModel.Purpose = visitor.Purpose;
                visitorModel.Status = visitor.Status;
                visitorModel.EntryTime = visitor.EntryTime;
                visitorModel.ExitTime = visitor.ExitTime;

                visitorModels.Add(visitorModel);
            }

            return visitorModels;
        }

        public async Task<VisitorDto> UpdateVisitor(VisitorDto visitorDto)
        {
            // Retrieve the existing visitor by UId
            var existingVisitor = await _cosmosDBService.GetVisitorByUId(visitorDto.UId);

            // If the visitor is not found, throw an exception
            if (existingVisitor == null)
            {
                throw new InvalidOperationException("Visitor not found.");
            }

            // Update the existing visitor's properties with the new values
            existingVisitor.Name = visitorDto.Name;
            existingVisitor.Email = visitorDto.Email;
            existingVisitor.PhoneNumber = visitorDto.PhoneNumber;
            existingVisitor.Address = visitorDto.Address;
            existingVisitor.CompanyName = visitorDto.CompanyName;
            existingVisitor.Purpose = visitorDto.Purpose;
            existingVisitor.Status = visitorDto.Status;
            existingVisitor.EntryTime = visitorDto.EntryTime;
            existingVisitor.ExitTime = visitorDto.ExitTime;
            existingVisitor.UpdatedBy = "ABC"; // Update this value as needed
            existingVisitor.UpdatedOn = DateTime.Now;

            // Update the visitor in the database
            var updatedVisitor = await _cosmosDBService.UpdateVisitor(existingVisitor);

            // Create a response model with the updated visitor's details
            var responseModel = new VisitorDto
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

        public async Task<string> DeleteVisitor(string uid)
        {
            var visitor = await _cosmosDBService.GetVisitorByUId(uid);
            visitor.Active = false;
            visitor.Archived = true;
            await _cosmosDBService.ReplaceAsync(visitor);

            //Assign values to manadatory fields
            visitor.Id = Guid.NewGuid().ToString();
            visitor.UId = visitor.Id;
            visitor.DocumentType = Credentials.VisitorDocumentType;
            visitor.CreatedBy = "ABC";
            visitor.CreatedOn = DateTime.Now;
            visitor.UpdatedBy = "";
            visitor.UpdatedOn = DateTime.Now;
            visitor.Version = 1;
            visitor.Active = false;
            visitor.Archived = true;

            return (visitor.Name + " is deleted successfully!!");
        }



    }
}
