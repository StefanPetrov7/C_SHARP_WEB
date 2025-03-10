using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CinemaApp.Data.Configuration
{
    public class ApplicationUserMovieConfiguration : IEntityTypeConfiguration<ApplicationUserMovie>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserMovie> builder)
        {
            builder.HasKey(x => new { x.ApplicationUserId, x.MovieId });

            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.ApplicationUserMovies)
                .HasForeignKey(x => x.ApplicationUserId);


            builder.HasOne(x => x.Movie)
                .WithMany(x => x.MovieApplicationUsers)
                .HasForeignKey(x => x.MovieId);

        }
    }
}


