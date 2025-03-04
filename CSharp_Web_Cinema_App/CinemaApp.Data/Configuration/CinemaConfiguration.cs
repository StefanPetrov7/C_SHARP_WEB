using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CinemaApp.Data.Models;
using static CinemaApp.Common.EntityValidationConstants.Cinema;


namespace CinemaApp.Data.Configuration
{
    public class CinemaConfiguration : IEntityTypeConfiguration<Cinema>
    {
        public void Configure(EntityTypeBuilder<Cinema> builder)
        {

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(CinemaNameMaxLength);

            builder.Property(c => c.Location)
                .IsRequired()
                .HasMaxLength(CinemaLocationMaxLength);

            builder.HasData(this.SeedCinema());

        }

        private IEnumerable<Cinema> SeedCinema()
        {
            IEnumerable<Cinema> cinemas = new List<Cinema>()
             {
                 new Cinema()
                 {
                    Name = "Cinema City",
                    Location = "Sofia",
                 },

                 new Cinema()
                 {
                    Name = "Cinema City",
                    Location = "Plovdiv",
                 },

                 new Cinema()
                 {
                    Name = "Cinema City",
                    Location = "Ruse",
                 },

                 new Cinema()
                 {
                    Name = "Cinemax",
                    Location = "Varna",
                 },

             };

            return cinemas;

        }
    }
}
