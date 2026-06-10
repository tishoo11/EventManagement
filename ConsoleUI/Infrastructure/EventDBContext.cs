using Event_Management_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.ConsoleUI.Infrastructure
{
    internal class EventDBContext : DbContext
    {
        public EventDBContext()
        {

        }

        public EventDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

           => optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=EventManagementDB;Integrated Security=True;");

        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
