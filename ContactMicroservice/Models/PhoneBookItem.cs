using System;

namespace ContactMicroservice.Models
{
    public class PhoneBookItem
    {
        public int PhoneBookItemId { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
