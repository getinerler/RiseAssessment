using ContactMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactMicroservice.Service
{
    public interface IPhoneBookService
    {
        Task<List<PhoneBookItemForListDto>> GetList();
        Task<PhoneBookItemDetailDto> Get(Guid id);
        Task<Guid> Save(PhoneBookItemAddDto item);
        Task Delete(Guid id);
        Task Update(Guid id, string key, string value);
        Task<List<ReportInfoItemDto>> GetReportInfo();
    }
}
