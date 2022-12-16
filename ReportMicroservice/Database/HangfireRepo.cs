using Microsoft.EntityFrameworkCore;
using ReportMicroservice.Models;
using System;
using System.Threading.Tasks;

namespace ReportMicroservice.Database
{
    public class HangfireRepo : IHangfireRepo
    {
        private readonly DataContext _db;

        public HangfireRepo(DataContext db)
        {
            _db = db;
        }

        public async Task UpdateStatus(Guid guid)
        {
            Report report = await _db.Reports.FirstOrDefaultAsync(x => x.Guid.Equals(guid));
            report.ExcelFileReady = true;
            await _db.SaveChangesAsync();
        }
    }
}
