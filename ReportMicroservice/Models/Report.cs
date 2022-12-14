using System;

namespace ReportMicroservice.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool ExcelFileReady { get; set; }
    }
}
