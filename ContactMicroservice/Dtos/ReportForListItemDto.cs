using System;

namespace ContactMicroservice.Dtos
{
    public class ReportForListItemDto
    {
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
