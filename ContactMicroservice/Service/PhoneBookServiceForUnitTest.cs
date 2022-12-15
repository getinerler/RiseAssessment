using ContactMicroservice.Dtos;
using ContactMicroservice.Exceptions;
using ContactMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public class PhoneBookServiceForUnitTest : IPhoneBookService
    {
        private readonly List<PhoneBookItem> list;

        public PhoneBookServiceForUnitTest()
        {
            list = new List<PhoneBookItem>()
            {
                new PhoneBookItem()
                {
                    PhoneBookItemId = 1,
                    Guid = new Guid("ec2d9314-34e1-4f4e-9d03-a877920dfb5a"),
                    Name = "Paul",
                    Surname = "McCartney",
                    Firm = "Beatles",
                    Phone = "+905301533020",
                    Mail = "paulmccartney@beatles.com",
                    Country = "England",
                    City = "Liverpool",
                    CreatedDate = new DateTime(1942, 06, 18),
                    IsDeleted = false
                },
                new PhoneBookItem()
                {
                    PhoneBookItemId = 2,
                    Guid = new Guid("3a165fef-1b99-4077-8885-f8f74e180cb1"),
                    Name = "John",
                    Surname = "Lennon",
                    Firm = "Beatles",
                    Phone = "+905301533010",
                    Mail = "johnlennon@beatles.com",
                    Country = "England",
                    City = "Liverpool",
                    CreatedDate = new DateTime(1940, 10, 09),
                    IsDeleted = false
                },
                new PhoneBookItem()
                {
                    PhoneBookItemId = 3,
                    Guid = new Guid("06a25f72-3e79-49ce-a95f-ab982ee43ded"),
                    Name = "Mick",
                    Surname = "Jagger",
                    Firm = "Rolling Stones",
                    Phone = "+905301533020",
                    Mail = "paulmccartney@beatles.com",
                    Country = "England",
                    City = "London",
                    CreatedDate = new DateTime(2020, 01, 02),
                    IsDeleted = false
                }
            };
        }

        public async Task Delete(Guid guid)
        {
            PhoneBookItem item = list.FirstOrDefault(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new ItemNotFoundException(guid);
            }
            list.Remove(item);
        }

        public async Task<PhoneBookItemDetailDto> Get(Guid guid)
        {
            PhoneBookItem item = list.FirstOrDefault(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new ItemNotFoundException(guid);
            }
            return new PhoneBookItemDetailDto()
            {
                Guid = item.Guid,
                Name = item.Name,
                Surname = item.Surname,
                Firm = item.Firm,
                Phone = item.Phone,
                Mail = item.Mail,
                Country = item.Country,
                City = item.City,
            };
        }

        public async Task<List<PhoneBookItemForListDto>> GetList()
        {
            return list
                .Select(x => new PhoneBookItemForListDto()
                {
                    Name = x.Name + " " + x.Surname,
                    Guid = x.Guid
                })
                .ToList();
        }

        public async Task<List<ReportInfoItemDto>> GetReportInfo()
        {
            var resultList =
                (from phones in list
                 group phones by new { phones.Country, phones.City } into g
                 select new ReportInfoItemDto
                 {
                     City = g.Key.City,
                     Country = g.Key.Country,
                     Count = g.Count()
                 })
                 .ToList();

            return resultList;
        }

        public async Task<ReportInfoDto> GetReportInfo(Guid guid)
        {
            return new ReportInfoDto()
            {
                Path = "/path/example/excel.xlsx",
                Status = "Completed"
            };
        }

        public async Task<List<ReportForListItemDto>> GetReports()
        {
            List<ReportForListItemDto> items = new List<ReportForListItemDto>(100);
            for (int i = 0; i < 100; i++)
            {
                items.Add(new ReportForListItemDto()
                {
                    CreatedDate = DateTime.Now,
                    Guid = Guid.NewGuid()
                });
            }
            return items;
        }

        public async Task<Guid> GetRequest()
        {
            return Guid.NewGuid();
        }

        public async Task<Guid> Save(PhoneBookItemAddDto item)
        {
            Guid newGuid = Guid.NewGuid();

            PhoneBookItem newItem = new PhoneBookItem()
            {
                PhoneBookItemId = list.Max(x => x.PhoneBookItemId) + 1,
                Guid = newGuid,
                Name = item.Name,
                Surname = item.Surname,
                Firm = item.Firm,
                Phone = item.Phone,
                Mail = item.Mail,
                Country = item.Country,
                City = item.City,
                CreatedDate = DateTime.Now,
                IsDeleted = false,
            };
            list.Add(newItem);
            return newItem.Guid;
        }

        public async Task Update(Guid guid, string key, string value)
        {
            PhoneBookItem item = list.FirstOrDefault(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new Exception("No item with guid: " + guid);
            }

            if (key == "Name") item.Name = value;
            if (key == "Surname") item.Surname = value;
            if (key == "Firm") item.Firm = value;
            if (key == "Phone") item.Phone = value;
            if (key == "Mail") item.Mail = value;
            if (key == "Country") item.Country = value;
            if (key == "City") item.City = value;
        }
    }
}
