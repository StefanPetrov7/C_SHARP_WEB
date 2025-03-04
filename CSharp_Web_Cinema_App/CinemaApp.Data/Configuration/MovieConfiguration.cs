using CinemaApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Fluent API

            builder.HasKey(movie => movie.Id);

            builder.Property(movie => movie.Title)
                 .IsRequired()
                 .HasMaxLength(TitleMaxLength);

            builder.Property(movie => movie.Genre)
                .IsRequired()
                .HasMaxLength(GenreMaxLength);

            builder.Property(movie => movie.Director)
                .IsRequired()
                .HasMaxLength(DirectorNameMaxLength);

            builder.Property(movie => movie.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder.HasData(this.SeedMovies());

        }

        public List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie
                {
                    Title = "Harry Potter",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2005,11,01),
                    Director = "Mike Newel",
                    Duration = 157,
                    Description = "Fantasy movie about kids and magic"
                },

                new Movie
                {
                    Title = "Harry Potter 2",
                    Genre = "Fantasy",
                    ReleaseDate = new DateTime(2006,11,01),
                    Director = "Mike Newel",
                    Duration = 147,
                    Description = "Fantasy movie about kids and magic"
                }

            };

            return movies;

        }

    }

}




