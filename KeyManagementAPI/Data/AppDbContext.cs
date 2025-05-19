using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KeyManagementAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace KeyManagementAPI.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Key> Keys { get; set; }
    }
}
