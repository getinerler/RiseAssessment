using ReportMicroservice.Dtos;
using ReportMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportMicroservice.Database
{
    public interface IReportRepo
    {
        Task<Report> CreateReport();
        Task<Report> GetReport(Guid guid);
        Task<List<ReportForListDto>> GetReports();
    }
}
