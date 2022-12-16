using Microsoft.EntityFrameworkCore;
using ReportMicroservice.Models;

namespace ReportMicroservice.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(
        //        "User ID=postgres;Password=348348;Server=localhost;Port=5432;Database=ReportMicroservice;" +
        //        "Integrated Security=true;Pooling=true;");
        //}

        public DbSet<Report> Reports { get; set; }
    }
}
