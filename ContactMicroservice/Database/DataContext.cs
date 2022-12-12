using ContactMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactMicroservice.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<PhoneBookItem> PhoneBookItems { get; set; }
    }
}
