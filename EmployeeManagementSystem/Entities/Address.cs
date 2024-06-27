using Newtonsoft.Json;

namespace EmployeeManagementSystem.Entities
{
    public class Address
    {
        [JsonProperty(PropertyName = "city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        [JsonProperty(PropertyName = "region", NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }
        [JsonProperty(PropertyName = "postalCode", NullValueHandling = NullValueHandling.Ignore)]
        public string PostalCode { get; set; }
        [JsonProperty(PropertyName = "country", NullValueHandling = NullValueHandling.Ignore)]
        public string Country { get; set; }

    }
}
