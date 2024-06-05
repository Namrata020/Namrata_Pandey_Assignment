using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entities;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class OfficeService : IOfficeService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public  OfficeService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<OfficeDto> AddOffice(OfficeDto officeDto)
        {
            var existingOffice = await _cosmosDBService.GetOfficeByUId(officeDto.UId);
            if (existingOffice != null)
            {
                throw new InvalidOperationException("A office person with this email already exists.");
            }


            OfficeEntity office = new OfficeEntity();
            office.Name = officeDto.Name;
            office.Email = officeDto.Email;
            office.PhoneNumber = officeDto.PhoneNumber;
            office.Address = officeDto.Address;
            office.CompanyName = officeDto.CompanyName;
            office.Purpose = officeDto.Purpose;
            office.Status = officeDto.Status;
            office.EntryTime = officeDto.EntryTime;
            office.ExitTime = officeDto.ExitTime;

            //Assign values to manadatory fields
            office.Id = Guid.NewGuid().ToString();
            office.UId = office.Id;
            office.DocumentType = Credentials.VisitorDocumentType;
            office.CreatedBy = "ABC";
            office.CreatedOn = DateTime.Now;
            office.UpdatedBy = "";
            office.UpdatedOn = DateTime.Now;
            office.Version = 1;
            office.Active = true;
            office.Archived = false;

            var response = await _cosmosDBService.AddOffice(office);

            var responseModel = new OfficeDto();

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

        public async Task<string> DeleteOffice(string uid)
        {
            var office = await _cosmosDBService.GetOfficeByUId(uid);
            office.Active = false;
            office.Archived = true;
            await _cosmosDBService.ReplaceAsync(office);

            //Assign values to manadatory fields
            office.Id = Guid.NewGuid().ToString();
            office.UId = office.Id;
            office.DocumentType = Credentials.VisitorDocumentType;
            office.CreatedBy = "ABC";
            office.CreatedOn = DateTime.Now;
            office.UpdatedBy = "";
            office.UpdatedOn = DateTime.Now;
            office.Version = 1;
            office.Active = false;
            office.Archived = true;

            return (office.Name + " is deleted successfully!!");
        }

       

        public async Task<OfficeDto> UpdateOffice(OfficeDto office)
        {
            var existingOffice = await _cosmosDBService.GetOfficeByUId(office.UId);

           
            if (existingOffice == null)
            {
                throw new InvalidOperationException("Visitor not found.");
            }

            existingOffice.Name = office.Name;
            existingOffice.Email = office.Email;
            existingOffice.PhoneNumber = office.PhoneNumber;
            existingOffice.Address = office.Address;
            existingOffice.CompanyName = office.CompanyName;
            existingOffice.Purpose = office.Purpose;
            existingOffice.Status = office.Status;
            existingOffice.EntryTime = office.EntryTime;
            existingOffice.ExitTime = office.ExitTime;
            existingOffice.UpdatedBy = "ABC"; 
            existingOffice.UpdatedOn = DateTime.Now;

            var updatedOffice = await _cosmosDBService.UpdateOffice(existingOffice);

            var responseModel = new OfficeDto
            {
                UId = updatedOffice.UId,
                Name = updatedOffice.Name,
                Email = updatedOffice.Email,
                PhoneNumber = updatedOffice.PhoneNumber,
                Address = updatedOffice.Address,
                CompanyName = updatedOffice.CompanyName,
                Purpose = updatedOffice.Purpose,
                Status = updatedOffice.Status,
                EntryTime = updatedOffice.EntryTime,
                ExitTime = updatedOffice.ExitTime
            };

            return responseModel;
        }
    }
}
