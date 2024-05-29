using Newtonsoft.Json;

namespace LibraryManagementSystem.DTO
{
    public class MemberDto
    {
        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "dob", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
    }
}
