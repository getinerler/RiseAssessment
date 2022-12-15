using ReportMicroservice.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public interface IReportService
    {
        public Task<ReportInfo> GetReportInfo(Guid guid);
        public Task<List<ReportForListDto>> GetReports();
        public Task<Guid> RequestReport();
    }
}
