using EmployeeManagementSystem.DTO;
using Newtonsoft.Json;
using System.Net;

namespace EmployeeManagementSystem.Entities
{
    public class EmployeeBasicDetails : BaseEntity
    {
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
    }

    public class EmployeeFilterCriteria
    {
        public EmployeeFilterCriteria()
        {
            Filters = new List<FilterCriteria>();
            Employees = new List<EmployeeBasicDetailsDto>();
        }
        public int page { get; set; } //page number
        public int pageSize { get; set; } //records in one page
        public int TotalCount { get; set; } //total records in the db

        public List<FilterCriteria> Filters { get; set; } //pass filter
        public List<EmployeeBasicDetailsDto> Employees { get; set; }
    }


    public class FilterCriteria
    {
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
    }

}
