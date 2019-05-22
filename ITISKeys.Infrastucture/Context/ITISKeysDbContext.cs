using Microsoft.EntityFrameworkCore;
using ITISKeys.Models;

namespace ITISKeys.Infrastructure.Context
{
    public class ITISKeysDbContext : DbContext
    {
        public ITISKeysDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Room > Rooms { get; set; }

        public DbSet<Reservation> Reservations { get; set; }        
    }
}
