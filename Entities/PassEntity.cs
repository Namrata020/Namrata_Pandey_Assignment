using System.ComponentModel;

namespace VisitorSecurityClearanceSystem.Entities
{
    public class PassEntity
    {
        public int Id { get; set; }
        public int VisitorrId { get; set; }
        public string PdfPath { get; set; }
        public string Status { get; set; }
    }
}
