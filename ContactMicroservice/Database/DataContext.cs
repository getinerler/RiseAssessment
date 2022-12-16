using ContactMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactMicroservice.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhoneBookItem>().HasIndex(e => e.Guid);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql(
        //        "User ID=postgres;Password=test;Server=localhost;Port=5432;Database=ContactMicroservice;" +
        //        "Integrated Security=true;Pooling=true;");
        //}

        public DbSet<PhoneBookItem> PhoneBookItems { get; set; }
    }
}
