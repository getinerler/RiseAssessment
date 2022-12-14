using ContactMicroservice.Dtos;
using ContactMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Database
{
    public interface IPhoneBookRepo
    {
        public Task<List<PhoneBookItem>> GetItems();
        public Task<PhoneBookItem> GetItem(Guid guid);
        public Task<PhoneBookItem> Add(PhoneBookItemAddDto req);
        public Task Delete(Guid guid);
        public Task Update(Guid guid, string key, string value);
    }
}
