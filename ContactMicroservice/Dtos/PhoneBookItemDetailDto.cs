using System;

namespace ContactMicroservice.Dtos
{
    public class PhoneBookItemDetailDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Firm { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
