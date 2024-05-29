using Newtonsoft.Json;

namespace LibraryManagementSystem.DTO
{
    public class BookDto
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "author", NullValueHandling = NullValueHandling.Ignore)]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "publishedDatee", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime PublishedDate { get; set; }
        [JsonProperty(PropertyName = "isbn", NullValueHandling = NullValueHandling.Ignore)]
        public string ISBN { get; set; }
        [JsonProperty(PropertyName = "isIssued", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsIssued { get; set; }
    }
}
