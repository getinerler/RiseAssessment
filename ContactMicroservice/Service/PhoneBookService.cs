using ContactMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public class PhoneBookService : IPhoneBookService
    {
        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PhoneBookItemDetailDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PhoneBookItemForListDto>> GetList()
        {
            throw new NotImplementedException();
        }

        public async Task<Guid> Save(PhoneBookItemAddDto item)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Guid id, string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
