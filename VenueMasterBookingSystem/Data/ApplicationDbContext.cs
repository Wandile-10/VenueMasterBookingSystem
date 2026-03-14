using Microsoft.EntityFrameworkCore;
using VenueMasterBookingSystem.Models;

namespace VenueMasterBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Booking> Bookings { get; set; }
    }
}