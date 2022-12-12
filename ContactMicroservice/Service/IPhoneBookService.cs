using ContactMicroservice.Dtos;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public interface IPhoneBookService
    {
        Task GetList();
        Task Get(int id);
        Task Save(PhoneBookItemAddDto item);
        Task Delete(int id);
        Task Update(int id, string key, string value);
    }
}
