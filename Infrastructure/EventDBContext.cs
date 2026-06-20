using EventManagement11.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventManagement11.Infrastructure
{
    public class EventDBContext : DbContext
    {
        public EventDBContext()
        {

        }

        public EventDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

           => optionsBuilder.UseSqlServer(@"Server=DESKTOP-N33VRK1\SQLEXPRESS;Database=EventManagementDB;Integrated Security=True;TrustServerCertificate=True;");

        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name).IsRequired().HasMaxLength(150);
                entity.Property(l => l.Address).IsRequired().HasMaxLength(250);
                entity.Property(l => l.Capacity).IsRequired();
            });

            modelBuilder.Entity<Organizer>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Name).IsRequired().HasMaxLength(150);
                entity.Property(o => o.ContactNumber).IsRequired().HasMaxLength(30);
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120); 
                entity.Property(e => e.EventType).IsRequired().HasMaxLength(80); 
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Capacity).IsRequired();

                
                entity.HasOne(e => e.Location)
                      .WithMany(l => l.Events)
                      .HasForeignKey(e => e.LocationId)
                      .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(e => e.Organizer)
                      .WithMany(o => o.Events)
                      .HasForeignKey(e => e.OrganizerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TicketType>(entity =>
            {
                entity.HasKey(tt => tt.Id);
                entity.Property(tt => tt.Name).IsRequired().HasMaxLength(100);

                entity.OwnsOne(tt => tt.Price, price =>
                {
                    price.Property(p => p.Amount)
                         .HasColumnName("Price") 
                         .HasColumnType("decimal(18,2)")
                         .IsRequired();
                });
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Code).IsRequired().HasMaxLength(100);
                entity.Property(t => t.SoldAt).IsRequired();

                entity.Property(t => t.Status).HasConversion<int>().IsRequired();

                entity.OwnsOne(t => t.Price, price =>
                {
                    price.Property(p => p.Amount)
                         .HasColumnName("Price")
                         .HasColumnType("decimal(18,2)")
                         .IsRequired();
                });

                entity.HasOne(t => t.TicketType)
                      .WithMany(tt => tt.Tickets)
                      .HasForeignKey(t => t.TicketTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Event>()
                      .WithMany(e => e.Tickets)
                      .HasForeignKey(t => t.EventId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
