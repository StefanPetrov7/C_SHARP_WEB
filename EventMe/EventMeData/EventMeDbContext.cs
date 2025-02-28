using EventMeDataModels;
using Microsoft.EntityFrameworkCore;

namespace EventMeData
{
    public class EventMeDbContext  : DbContext
    {
        public EventMeDbContext()
        {
            
        }

        public EventMeDbContext(DbContextOptions options) 
            : base(options)
        {
            
        }

        public DbSet<Event> Events { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Property set to have default value. This property will be used for deleting from the DB. => Soft Delete 

            modelBuilder.Entity<Event>()
                .Property(prop => prop.IsActive)
                .HasDefaultValue(true)
                .IsRequired(false);
               
        }
    }
}
 