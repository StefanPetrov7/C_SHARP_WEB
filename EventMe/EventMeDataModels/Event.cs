using System.ComponentModel.DataAnnotations;

using static EventMeCommon.EntityConstrains;

namespace EventMeDataModels
{
    public class Event
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [MaxLength(EventPlaceMaxLength)]
        public string Place { get; set; } = null!;

        // Even that it is a nullable value it is required as we have default value set up "true". [required null] => will go into the DB as [true], if it is not required will be saved as false. 
        [Required]
        public bool? IsActive { get; set; }

    }
}
