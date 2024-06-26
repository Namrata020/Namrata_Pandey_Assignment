namespace EmployeeManagementSystem.Common
{
    public class Credentials
    {
        public static readonly string databaseName = Environment.GetEnvironmentVariable("dataBaseName");
        public static readonly string containerName = Environment.GetEnvironmentVariable("containerName");
        public static readonly string CosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        internal static readonly string VisitorUrl = Environment.GetEnvironmentVariable("visitorUrl");
        internal static readonly string AddEmployeeEndpoint = "api/Employee/AddEmployeeBasicDetails";
        public static string EmployeeDocumentType = "employee";
    }
}
