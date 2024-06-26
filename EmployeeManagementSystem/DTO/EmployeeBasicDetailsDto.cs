using EmployeeManagementSystem.Entities;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.DTO
{
    public class EmployeeBasicDetailsDto
    {
        [JsonProperty(PropertyName = "uid", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }
        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }
        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }
        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }
        [JsonProperty(PropertyName = "empoyeeId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeID { get; set; }
        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }
        [JsonProperty(PropertyName = "reportingManagerId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerUId { get; set; }
        [JsonProperty(PropertyName = "reportingMangerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerName { get; set; }
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public Address Address { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }
}
