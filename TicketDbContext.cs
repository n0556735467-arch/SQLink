using Microsoft.EntityFrameworkCore;
using Entities;

namespace Repositories
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(t => t.Status)
                    .HasConversion<string>();

                entity.Property(t => t.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(t => t.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(t => t.Description)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}