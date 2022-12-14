using Microsoft.EntityFrameworkCore;
using ReportMicroservice.Exceptions;
using ReportMicroservice.Models;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Database
{
    public class ReportRepo : IReportRepo
    {
        private readonly DataContext _db;

        public ReportRepo(DataContext db)
        {
            _db = db;
        }

        public async Task<Report> GetReport(Guid guid)
        {
            return await _db.Reports.FirstOrDefaultAsync(x => x.Guid.Equals(guid));
        }

        public async Task SetPath(int id, string path)
        {
            Report report = await _db.Reports.FirstOrDefaultAsync(x => x.ReportId == id);
            if (report == null)
            {
                throw new ItemNotFoundException();
            }

            report.Path = path;
            await _db.SaveChangesAsync();
        }
    }
}
