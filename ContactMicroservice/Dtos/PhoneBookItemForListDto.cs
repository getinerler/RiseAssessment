using System;

namespace ContactMicroservice.Dtos
{
    public class PhoneBookItemForListDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
