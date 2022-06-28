using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resto_Backend.Models;

namespace Resto_Backend.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext( DbContextOptions options) : base(options)
        {
        }
        public DbSet<Breakfast> Breakfasts { get; set; }
        public DbSet<Bio> Bios { get; set; }
    }
}
