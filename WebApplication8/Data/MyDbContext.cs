using Microsoft.EntityFrameworkCore;
using WebApplication8.Models.Domain;

namespace WebApplication8.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cafe> Cafes { get; set; }
    }
}
