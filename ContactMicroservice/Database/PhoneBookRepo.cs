using ContactMicroservice.Dtos;
using ContactMicroservice.Exceptions;
using ContactMicroservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMicroservice.Database
{
    public class PhoneBookRepo : IPhoneBookRepo
    {
        private readonly DataContext _db;

        public PhoneBookRepo(DataContext db)
        {
            _db = db;
        }

        public async Task<PhoneBookItem> Add(PhoneBookItemAddDto req)
        {
            PhoneBookItem item = new PhoneBookItem()
            {
                Guid = Guid.NewGuid(),
                Name = req.Name,
                Surname = req.Surname,
                Firm = req.Firm,
                Phone = req.Phone,
                Mail = req.Mail,
                Country = req.Country,
                City = req.City,
                CreatedDate = DateTime.Now,
                IsDeleted = false
            };

            await _db.AddAsync(item);
            await _db.SaveChangesAsync();

            return item;
        }

        public async Task Delete(Guid guid)
        {
            PhoneBookItem item = _db.PhoneBookItems.FirstOrDefault(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new ItemNotFoundException(guid);
            }

            _db.PhoneBookItems.Remove(item);
            await _db.SaveChangesAsync();
        }

        public async Task<PhoneBookItem> GetItem(Guid guid)
        {
            PhoneBookItem item = await _db.PhoneBookItems.FirstOrDefaultAsync(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new ItemNotFoundException(guid);
            }
            return item;
        }

        public async Task<List<PhoneBookItem>> GetItems()
        {
            return await _db.PhoneBookItems.ToListAsync();
        }

        public async Task Update(Guid guid, string key, string value)
        {
            PhoneBookItem item = await _db.PhoneBookItems.FirstOrDefaultAsync(x => x.Guid.Equals(guid));
            if (item == null)
            {
                throw new ItemNotFoundException(guid);
            }

            if (key == "Name")
            {
                item.Name = value;
            }

            if (key == "Surname")
            {
                item.Surname = value;
            }

            if (key == "Firm")
            {
                item.Firm = value;
            }

            if (key == "Phone")
            {
                item.Phone = value;
            }

            if (key == "Mail")
            {
                item.Mail = value;
            }

            if (key == "Country")
            {
                item.Country = value;
            }

            if (key == "City")
            {
                item.City = value;
            }

            await _db.SaveChangesAsync();
        }

        public async Task<List<ReportInfoItemDto>> GetReportInfo()
        {
            var list = await
                (from phones in _db.PhoneBookItems
                 group phones by new { phones.Country, phones.City } into g
                 select new ReportInfoItemDto
                 {
                     City = g.Key.City,
                     Country = g.Key.Country,
                     Count = g.Count()
                 })
                 .ToListAsync();

            return list;
        }
    }
}
