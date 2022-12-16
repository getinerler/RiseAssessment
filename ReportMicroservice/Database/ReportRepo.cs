using Microsoft.EntityFrameworkCore;
using ReportMicroservice.Dtos;
using ReportMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public async Task<Report> CreateReport()
        {
            Report newReport = new Report()
            {
                CreatedDate = DateTime.Now,
                Guid = Guid.NewGuid()
            };

            await _db.AddAsync(newReport);
            await _db.SaveChangesAsync();

            return newReport;
        }

        public async Task<Report> GetReport(Guid guid)
        {
            return await _db.Reports.FirstOrDefaultAsync(x => x.Guid.Equals(guid));
        }

        public async Task<List<ReportForListDto>> GetReports()
        {
            return await _db.Reports
                .Select(x => new ReportForListDto() 
                {
                    Guid = x.Guid,
                    CreatedDate = x.CreatedDate,
                    Status = x.ExcelFileReady ? 
                        ReportStatus.Completed.ToString() : 
                        ReportStatus.Processing.ToString(),
                    Path = "/ExcelFiles/" + x.Guid.ToString() + ".xlsx"
                })
                .ToListAsync();
        }
    }
}
