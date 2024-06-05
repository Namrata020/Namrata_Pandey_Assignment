using VisitorSecurityClearanceSystem.Common;
using VisitorSecurityClearanceSystem.CosmosDB;
using VisitorSecurityClearanceSystem.DTO;
using VisitorSecurityClearanceSystem.Entities;
using VisitorSecurityClearanceSystem.Interface;

namespace VisitorSecurityClearanceSystem.Services
{
    public class SecurityService : ISecurityService
    {
        public readonly ICosmosDBService _cosmosDBService;

        public SecurityService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }
        public async Task<SecurityDto> AddSecurity(SecurityDto securityDto)
        {
            var existingSecurity = await _cosmosDBService.GetSecurityByUId(securityDto.UId);
            if (existingSecurity != null)
            {
                throw new InvalidOperationException("A security person with this email already exists.");
            }


            SecurityEntity security = new SecurityEntity();
            security.Name = securityDto.Name;
            security.Email = securityDto.Email;
            security.PhoneNumber = securityDto.PhoneNumber;
            security.Address = securityDto.Address;
            security.CompanyName = securityDto.CompanyName;
            security.Purpose = securityDto.Purpose;
            security.Status = securityDto.Status;
            security.EntryTime = securityDto.EntryTime;
            security.ExitTime = securityDto.ExitTime;

            //Assign values to manadatory fields
            security.Id = Guid.NewGuid().ToString();
            security.UId = security.Id;
            security.DocumentType = Credentials.VisitorDocumentType;
            security.CreatedBy = "ABC";
            security.CreatedOn = DateTime.Now;
            security.UpdatedBy = "";
            security.UpdatedOn = DateTime.Now;
            security.Version = 1;
            security.Active = true;
            security.Archived = false;

            var response = await _cosmosDBService.AddSecurity(security);

            var responseModel = new SecurityDto();

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

        public async Task<string> DeleteSecurity(string uid)
        {
            var security = await _cosmosDBService.GetVisitorByUId(uid);
            security.Active = false;
            security.Archived = true;
            await _cosmosDBService.ReplaceAsync(security);

            //Assign values to manadatory fields
            security.Id = Guid.NewGuid().ToString();
            security.UId = security.Id;
            security.DocumentType = Credentials.VisitorDocumentType;
            security.CreatedBy = "ABC";
            security.CreatedOn = DateTime.Now;
            security.UpdatedBy = "";
            security.UpdatedOn = DateTime.Now;
            security.Version = 1;
            security.Active = false;
            security.Archived = true;

            return (security.Name + " is deleted successfully!!");
        }

        public async Task<SecurityDto> UpdateSecurity(SecurityDto security)
        {
            var existingSecurity = await _cosmosDBService.GetSecurityByUId(security.UId);

            if (existingSecurity == null)
            {
                throw new InvalidOperationException("Visitor not found.");
            }

            existingSecurity.Name = security.Name;
            existingSecurity.Email = security.Email;
            existingSecurity.PhoneNumber = security.PhoneNumber;
            existingSecurity.Address = security.Address;
            existingSecurity.CompanyName = security.CompanyName;
            existingSecurity.Purpose = security.Purpose;
            existingSecurity.Status = security.Status;
            existingSecurity.EntryTime = security.EntryTime;
            existingSecurity.ExitTime = security.ExitTime;
            existingSecurity.UpdatedBy = "ABC"; 
            existingSecurity.UpdatedOn = DateTime.Now;

            var updatedSecurity = await _cosmosDBService.UpdateSecurity(existingSecurity);

            var responseModel = new SecurityDto
            {
                UId = updatedSecurity.UId,
                Name = updatedSecurity.Name,
                Email = updatedSecurity.Email,
                PhoneNumber = updatedSecurity.PhoneNumber,
                Address = updatedSecurity.Address,
                CompanyName = updatedSecurity.CompanyName,
                Purpose = updatedSecurity.Purpose,
                Status = updatedSecurity.Status,
                EntryTime = updatedSecurity.EntryTime,
                ExitTime = updatedSecurity.ExitTime
            };

            return responseModel;
        }

        //public async Task<SecurityDto> GetSecuritieByUId(string uid)
        //{
            //var security = await _cosmosDBService.GetSecurityByUId(uid);

            //return security;           
        //}

    }
}
