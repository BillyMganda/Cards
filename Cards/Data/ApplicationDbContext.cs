using Cards.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
