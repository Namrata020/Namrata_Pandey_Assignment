using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class MemberController : Controller
    {

        public Container Container;

        public MemberController()
        {
            Container = GetContainer();
        }

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


        [HttpGet]
        public async Task<List<MemberDto>> GetAllMembers()
        {
            //fetch all records
            var members = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType == "library").ToList();

            List<MemberDto> memberDtos = new List<MemberDto>();
            
            foreach (var member in members)
            {
                MemberDto model=new MemberDto();
                model.UId= member.UId;
                model.Name= member.Name;
                model.DateOfBirth = member.DateOfBirth;
                model.Email = member.Email;

                memberDtos.Add(model);
            }

            return memberDtos;

        }

        [HttpPost]
        public async Task<MemberDto> AddMemberEntity(MemberDto memberDto)
        {
            MemberEntity member= new MemberEntity();
            member.Name= memberDto.Name;
            member.DateOfBirth= memberDto.DateOfBirth;
            member.Email= memberDto.Email;

            member.Id=Guid.NewGuid().ToString();
            member.UId = member.Id;
            member.DocumentType = "library";
            member.CreatedBy = "Isha";
            member.CreatedOn = DateTime.Now;
            member.UpdatedBy = "";
            member.UpdatedOn = DateTime.Now;
            member.Version = 1;
            member.Active = true;
            member.Archived = false;

            MemberEntity response=await Container.CreateItemAsync(member);

            MemberDto responseModel=new MemberDto();
            responseModel.Name = response.Name;
            responseModel.DateOfBirth= response.DateOfBirth;
            responseModel.Email = response.Email;

            return responseModel;

        }


        [HttpGet]
        public async Task<MemberDto> GetMemberByUId(string UId)
        {
            var member = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId
             == UId && q.Active == true && q.Archived == false).FirstOrDefault();

            MemberDto memberDto = new MemberDto();
            memberDto.UId = member.UId;
            memberDto.Name = member.Name;
            memberDto.DateOfBirth = member.DateOfBirth;
            memberDto.Email = member.Email;

            return memberDto;

        }

        [HttpPost]
        public async Task<MemberDto> UpdateMember(MemberDto member)
        {
            var existingMember = Container.GetItemLinqQueryable<MemberEntity>(true).Where(q => q.UId == member.UId && q.Active == true && q.Archived == false).FirstOrDefault();

            existingMember.Archived = true;
            existingMember.Active = false;
            await Container.ReplaceItemAsync(existingMember, existingMember.Id);

            existingMember.Id = Guid.NewGuid().ToString();
            existingMember.UpdatedBy = "ABC";
            existingMember.UpdatedOn = DateTime.Now;
            existingMember.Version = existingMember.Version + 1;
            existingMember.Active = true;
            existingMember.Archived = false;

            existingMember.Name = member.Name;
            existingMember.DateOfBirth = member.DateOfBirth;
            existingMember.Email = member.Email;


            existingMember = await Container.CreateItemAsync(existingMember);

            MemberDto response = new MemberDto();
            response.UId = existingMember.UId;
            response.Name = existingMember.Name;
            response.DateOfBirth = member.DateOfBirth;
            response.Email = existingMember.Email;


            return response;

        }
















    }
}
