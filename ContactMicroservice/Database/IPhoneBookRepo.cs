using ContactMicroservice.Dtos;
using ContactMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Database
{
    public interface IPhoneBookRepo
    {
        Task<List<PhoneBookItem>> GetItems();
        Task<PhoneBookItem> GetItem(Guid guid);
        Task<PhoneBookItem> Add(PhoneBookItemAddDto req);
        Task Delete(Guid guid);
        Task Update(Guid guid, string key, string value);
        Task<List<ReportInfoItemDto>> GetReportInfo();
    }
}
