using CinemaApp.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;


namespace CinemaApp.Data
{
    public class CinemaMovie
    {
        public Guid CinemaId { get; set; }

        [ForeignKey(nameof(CinemaId))]
        public virtual Cinema Cinema { get; set; } = null!;


        public Guid MovieId { get; set; }

        [ForeignKey(nameof(MovieId))]
        public virtual Movie Movie { get; set; } = null!;
    }
}
