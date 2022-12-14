using System;

namespace ReportMicroservice.Dtos
{
    public class PhoneBookItem
    {
        public int PhoneBookItemId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNo { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
