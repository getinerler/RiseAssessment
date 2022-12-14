using ReportMicroservice.Dtos;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public interface IReportService
    {
        public Task<ReportInfo> GetReportInfo(Guid guid);
    }
}
