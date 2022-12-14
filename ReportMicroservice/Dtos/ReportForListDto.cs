using System;

namespace ReportMicroservice.Dtos
{
    public class ReportForListDto
    {
        public Guid Guid { get; set; }
        public string Path { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
