using ContactMicroservice.Database;
using ContactMicroservice.Dtos;
using ContactMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly IPhoneBookRepo _repo;

        public PhoneBookService(IPhoneBookRepo repo)
        {
            _repo = repo;
        }

        public async Task Delete(Guid id)
        {
            await _repo.Delete(id);
        }

        public async Task<PhoneBookItemDetailDto> Get(Guid id)
        {
            PhoneBookItem item = await _repo.GetItem(id);

            return new PhoneBookItemDetailDto()
            {
                Guid = item.Guid,
                Name = item.Name,
                Surname = item.Surname,
                Firm = item.Firm,
                Phone = item.Phone,
                Mail = item.Mail,
                Country = item.Country,
                City = item.City
            };
        }

        public async Task<List<PhoneBookItemForListDto>> GetList()
        {
            List<PhoneBookItem> list = await _repo.GetItems();

            return list
                .Select(x => new PhoneBookItemForListDto()
                {
                    Guid = x.Guid,
                    Name = x.Name
                })
                .ToList();
        }

        public async Task<Guid> Save(PhoneBookItemAddDto item)
        {
            PhoneBookItem newItem = await _repo.Add(item);
            return newItem.Guid;
        }

        public async Task<List<ReportInfoItemDto>> GetReportInfo()
        {
            return await _repo.GetReportInfo();
        }

        public async Task Update(Guid id, string key, string value)
        {
            await _repo.Update(id, key, value);
        }
    }
}
