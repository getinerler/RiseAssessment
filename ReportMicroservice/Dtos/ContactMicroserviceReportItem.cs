using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportMicroservice.Dtos
{
    public class ContactMicroserviceReportItem
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int Count { get; set; }
    }
}
