using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.DTO;
using Newtonsoft.Json;

namespace EmployeeManagementSystem.Entities
{
    public class EmployeeAdditionalDetails : BaseEntity
    {
        [JsonProperty(PropertyName = "employeeBasicDetailsUId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeBasicDetailsUId { get; set; }
        [JsonProperty(PropertyName = "alternateEmail", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateEmail { get; set; }
        [JsonProperty(PropertyName = "alternateMobile", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateMobile { get; set; }
        [JsonProperty(PropertyName = "workInformation", NullValueHandling = NullValueHandling.Ignore)]
        public WorkInfo_ WorkInformation { get; set; }
        [JsonProperty(PropertyName = "personalDetails", NullValueHandling = NullValueHandling.Ignore)]
        public PersonalDetails_ PersonalDetails { get; set; }
        [JsonProperty(PropertyName = "identityInformation", NullValueHandling = NullValueHandling.Ignore)]
        public IdentityInfo_ IdentityInformation { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }
    }


    public class AdditionalEmployeeFilterCriteria
    {
        public AdditionalEmployeeFilterCriteria()
        {
            Filters = new List<FilterCriteria>();
            AdditionalEmployees = new List<EmployeeAdditionalDetailsDto>();
        }

        public int Page { get; set; } // page number
        public int PageSize { get; set; } // records in one page
        public int TotalCount { get; set; } // total records in the db

        public List<FilterCriteria> Filters { get; set; } // pass filter
        public List<EmployeeAdditionalDetailsDto> AdditionalEmployees { get; set; }
    }
}
