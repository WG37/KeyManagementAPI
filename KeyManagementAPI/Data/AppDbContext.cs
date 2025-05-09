using Microsoft.EntityFrameworkCore;
using KeyManagementAPI.Entities;

namespace KeyManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Key> Keys { get; set; }
    }
}
