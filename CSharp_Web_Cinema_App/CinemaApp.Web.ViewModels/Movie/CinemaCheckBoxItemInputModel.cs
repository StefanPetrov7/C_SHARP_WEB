using System.ComponentModel.DataAnnotations;

using static CinemaApp.Common.EntityValidationConstants.Cinema;


namespace CinemaApp.Web.ViewModels.Movie
{
    public class CinemaCheckBoxItemInputModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [MinLength(CinemaNameMinLength)]
        [MaxLength(CinemaNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(CinemaLocationMinLength)]
        [MaxLength(CinemaLocationMaxLength)]
        public string Location { get; set; } = null!;

        // False and required by default. 
        [Required]
        public bool IsSelected { get; set; }
    }
}
