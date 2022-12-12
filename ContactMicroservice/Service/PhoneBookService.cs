using ContactMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public class PhoneBookService : IPhoneBookService
    {
        public async Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PhoneBookItemDetailDto> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhoneBookItemForListDto>> GetList()
        {
            throw new NotImplementedException();
        }

        public async Task Save(PhoneBookItemAddDto item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
