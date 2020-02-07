using Microsoft.EntityFrameworkCore;
using LoginRegistration.Models;


namespace LoginRegistration.Models
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions options) : base(options){}

        public DbSet<User> Users { get; set; }

        public DbSet<Wedding> Weddings { get; set; }
    }
}