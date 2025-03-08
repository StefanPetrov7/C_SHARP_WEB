using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class CinemaMovieConfiguration : IEntityTypeConfiguration<CinemaMovie>
    {
        public void Configure(EntityTypeBuilder<CinemaMovie> builder)
        {
            builder.HasKey(x => new { x.MovieId, x.CinemaId });

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(m => m.Movie)
                .WithMany(m => m.MovieCinemas)
                .HasForeignKey(m => m.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Cinema)
             .WithMany(c => c.CinemaMovies)
             .HasForeignKey(c => c.CinemaId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
