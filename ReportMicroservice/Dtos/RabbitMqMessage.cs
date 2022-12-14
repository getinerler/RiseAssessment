using System;

namespace ReportMicroservice.Dtos
{
    public class RabbitMqMessage
    {
        public Guid Guid { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
