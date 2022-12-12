using ContactMicroservice.Dtos;
using System;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public class PhoneBookService : IPhoneBookService
    {
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task GetList()
        {
            throw new NotImplementedException();
        }

        public Task Save(PhoneBookItemAddDto item)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
