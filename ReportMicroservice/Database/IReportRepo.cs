using ReportMicroservice.Models;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Database
{
    public interface IReportRepo
    {
        public Task SetPath(int id, string path);
        public Task<Report> GetReport(Guid guid);
    }
}
