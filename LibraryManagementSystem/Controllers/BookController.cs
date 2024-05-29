using LibraryManagementSystem.DTO;
using LibraryManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class BookController : Controller
    {
        //DB Connectivity
        public string URI = "https://localhost:8081";
        public string PrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseName = "Batch4";
        public string ContainerName = "Library";

        private Container GetContainer()
        {
            CosmosClient cosmosClient=new CosmosClient(URI,PrimaryKey);
            Database databse = cosmosClient.GetDatabase(DatabaseName);
            Container container=databse.GetContainer(ContainerName);
            return container;
        }

        public Container Container;
        public BookController()
        {
            Container=GetContainer();
        }

        [HttpPost]
        public async Task<BookDto> AddBookEntity(BookDto bookDto)
        {
            BookEntity book=new BookEntity();
            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.PublishedDate = bookDto.PublishedDate;
            book.ISBN = bookDto.ISBN;
            book.IsIssued = bookDto.IsIssued;

            book.Id=Guid.NewGuid().ToString();
            book.UId = book.Id;
            book.DocumentType = "library";
            book.CreatedBy = "XYZ";
            book.CreatedOn = DateTime.Now;
            book.UpdatedBy = "ABC";
            book.UpdatedOn = DateTime.Now;
            book.Version = 1;
            book.Active = true;
            book.Archived = false;

            BookEntity response=await Container.CreateItemAsync(book);

            BookDto responseModel=new BookDto();
            responseModel.Title = response.Title;
            responseModel.Author = response.Author;
            responseModel.PublishedDate = response.PublishedDate;
            responseModel.ISBN = response.ISBN;
            responseModel.IsIssued= response.IsIssued;

            return responseModel;

        }


        [HttpGet]
        public async Task<BookDto> GetBookByUId(string UId)
        {
            var book=Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId
             == UId && q.Active==true && q.Archived==false).FirstOrDefault();

            BookDto bookDto=new BookDto();
            bookDto.UId=book.UId;
            bookDto.Title = book.Title;
            bookDto.Author = book.Author;
            bookDto.PublishedDate = book.PublishedDate;
            bookDto.ISBN = book.ISBN;
            bookDto.IsIssued = book.IsIssued;

            return bookDto;

        }

        [HttpGet]
        public async Task<BookDto> GetBookByName(string name)
        {
            var book = Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Title
             == name && q.Active == true && q.Archived == false).FirstOrDefault();

            BookDto bookDto = new BookDto();
            bookDto.UId = book.UId;
            bookDto.Title = book.Title;
            bookDto.Author = book.Author;
            bookDto.PublishedDate = book.PublishedDate;
            bookDto.ISBN = book.ISBN;
            bookDto.IsIssued= book.IsIssued;

            return bookDto;

        }

        [HttpGet]
        public async Task<List<BookDto>> GetAllBooks()
        {
            var books=Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.Active == true && q.Archived == false && q.DocumentType=="library").ToList();

            List<BookDto> bookDtos= new List<BookDto>();

            foreach (var book in books)
            {
                BookDto bookDto = new BookDto();
                bookDto.UId= book.UId;
                bookDto.Title = book.Title; 
                bookDto.Author = book.Author;
                bookDto.PublishedDate= book.PublishedDate;
                bookDto.ISBN= book.ISBN;
                bookDto.IsIssued = book.IsIssued;

                bookDtos.Add(bookDto);
            }

            return bookDtos;
        }

        [HttpPost]
        public async Task<BookDto> UpdateBook(BookDto book)
        {
            var existingBook=Container.GetItemLinqQueryable<BookEntity>(true).Where(q => q.UId==book.UId && q.Active == true && q.Archived==false).FirstOrDefault();

            existingBook.Archived = true;
            existingBook.Active = false;
            await Container.ReplaceItemAsync(existingBook, existingBook.Id);

            existingBook.Id=Guid.NewGuid().ToString();
            existingBook.UpdatedBy = "ABC";
            existingBook.UpdatedOn = DateTime.Now;
            existingBook.Version = existingBook.Version + 1;
            existingBook.Active = true;
            existingBook.Archived = false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.PublishedDate=book.PublishedDate;
            existingBook.ISBN= book.ISBN;
            existingBook.IsIssued = book.IsIssued == true;

            existingBook=await Container.CreateItemAsync(existingBook);

            BookDto response= new BookDto();
            response.UId=existingBook.UId;
            response.Title=existingBook.Title;
            response.Author=existingBook.Author;
            response.PublishedDate=book.PublishedDate;
            response.ISBN= book.ISBN;
            response.IsIssued= book.IsIssued == true;

            return response;

        }


























    }
}
