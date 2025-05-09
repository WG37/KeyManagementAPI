using Microsoft.EntityFrameworkCore;
using KeyManagementAPI.Entities;

namespace KeyManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        DbSet<Key> Keys { get; set; }
    }
}
