using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class IssueController : Controller
    {
        //DB Connectivity
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Batch4";
        public string ContainerName = "Library";

        private Container GetContainer()
        {
            CosmosClient cosmosClient = new CosmosClient(URI, PrimaryKey);
            Database databse = cosmosClient.GetDatabase(DatabaseName);
            Container container = databse.GetContainer(ContainerName);
            return container;
        }

        public Container Container;
        public IssueController()
        {
            Container = GetContainer();
        }

        [HttpPost]
        public async Task<IssueDto> AddIssueEntity(IssueDto issueDto)
        {
            IssueEntity issue = new IssueEntity();
            issue.BookId = issueDto.BookId;
            issue.MemberId = issueDto.MemberId;
            issue.IssueDate = issueDto.IssueDate;
            issue.ReturnDate = issueDto.ReturnDate;
            issue.isReturned = issueDto.isReturned;


            issue.Id = Guid.NewGuid().ToString();
            issue.UId = issue.Id;
            issue.DocumentType = "library";
            issue.CreatedBy = "XYZ";
            issue.CreatedOn = DateTime.Now;
            issue.UpdatedBy = "ABC";
            issue.UpdatedOn = DateTime.Now;
            issue.Version = 1;
            issue.Active = true;
            issue.Archived = false;

            IssueEntity response = await Container.CreateItemAsync(issue);

            IssueDto responseModel = new IssueDto();
            responseModel.BookId = issueDto.BookId;
            responseModel.MemberId = issueDto.MemberId;
            response.IssueDate = issueDto.IssueDate;
            responseModel.ReturnDate = issueDto.ReturnDate;
            responseModel.isReturned = issueDto.isReturned;

            return responseModel;

        }


        [HttpGet]
        public async Task<IssueDto> GetIssueByUId(string UId)
        {
            var issue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId
             == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            IssueDto issueDto = new IssueDto();
            issueDto.UId = issue.UId;
            issueDto.BookId = issue.BookId;
            issueDto.MemberId = issue.MemberId;
            issueDto.IssueDate = issue.IssueDate;
            issueDto.ReturnDate = issue.ReturnDate;
            issueDto.isReturned = issue.isReturned;

            return issueDto;

        }


        [HttpPost]
        public async Task<IssueDto> UpdateIssue(IssueDto issue)
        {
            var existingIssue = Container.GetItemLinqQueryable<IssueEntity>(true).Where(q => q.UId == issue.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingIssue.Archived = true;
            existingIssue.Active = false;
            await Container.ReplaceItemAsync(existingIssue, existingIssue.Id);

            existingIssue.Id = Guid.NewGuid().ToString();
            existingIssue.UpdatedBy = "ABC";
            existingIssue.UpdatedOn = DateTime.Now;
            existingIssue.Version = existingIssue.Version + 1;
            existingIssue.Active = true;
            existingIssue.Archived = false;

            existingIssue.BookId = issue.BookId;
            existingIssue.MemberId = issue.MemberId;
            existingIssue.IssueDate = issue.IssueDate;
            existingIssue.ReturnDate = issue.ReturnDate;
            existingIssue.isReturned = issue.isReturned;

            existingIssue = await Container.CreateItemAsync(existingIssue);

            IssueDto response = new IssueDto();
            response.UId = existingIssue.UId;
            response.BookId = existingIssue.BookId;
            response.MemberId = existingIssue.MemberId;
            response.IssueDate = existingIssue.IssueDate;
            response.ReturnDate = existingIssue.ReturnDate;
            response.isReturned = existingIssue.isReturned;

            return response;

        }
    }
}
