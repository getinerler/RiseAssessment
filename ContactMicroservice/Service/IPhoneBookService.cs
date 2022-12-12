using ContactMicroservice.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public interface IPhoneBookService
    {
        Task<List<PhoneBookItemForListDto>> GetList();
        Task<PhoneBookItemDetailDto> Get(int id);
        Task Save(PhoneBookItemAddDto item);
        Task Delete(int id);
        Task Update(int id, string key, string value);
    }
}
