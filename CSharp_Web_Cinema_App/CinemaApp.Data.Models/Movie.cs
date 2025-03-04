using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Data.Models
{
    public class Movie
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string Director { get; set; } = null!;

        public int Duration { get; set; }

        public string Description { get; set; } = null!;

        public virtual ICollection<CinemaMovie> MovieCinemas { get; set; } = new HashSet<CinemaMovie>();

    }
}
