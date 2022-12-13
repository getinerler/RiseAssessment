using System;

namespace ReportMicroservice.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public Guid Guid { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
