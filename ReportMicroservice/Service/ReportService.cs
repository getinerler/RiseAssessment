using ReportMicroservice.Database;
using ReportMicroservice.Dtos;
using ReportMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportMicroservice.Service
{
    public class ReportService : IReportService
    {
        private readonly IReportRepo _repo;

        public ReportService(IReportRepo repo)
        {
            _repo = repo;
        }

        public async Task<ReportInfo> GetReportInfo(Guid guid)
        {
            Report report = await _repo.GetReport(guid);

            if (report == null)
            {
                return new ReportInfo() 
                { 
                    Status = ReportStatus.NotQueued 
                };
            }

            ReportInfo info = new ReportInfo()
            {
                Status = string.IsNullOrEmpty(report.Path) ? 
                    ReportStatus.Processing : 
                    ReportStatus.Completed,
                Path = report.Path
            };

            return info;
        }

        public async Task<Guid> RequestReport(string country, string city)
        {
            Report newReport = await _repo.CreateReport();
            RabbitMQHelper.RabbitMQReceiveHelper.SendNewRequest(newReport.Guid, country, city);
            return newReport.Guid;
        }

        public async Task<List<ReportForListDto>> GetReports()
        {
            return await _repo.GetReports();
        }
    }
}
