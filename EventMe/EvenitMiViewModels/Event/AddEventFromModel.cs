namespace EventMiViewModels.Event
{
    using System.ComponentModel.DataAnnotations;
    using static EventMeCommon.EntityConstrains;
    public class AddEventFromModel
    {
        [Required]
        [StringLength(EventMaxLength, MinimumLength = EventMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        public string StartDate { get; set; } = null!;

        [Required]
        public string EndDate { get; set; } = null!;
         
        [Required]
        [StringLength(EventPlaceMaxLength, MinimumLength = EventPlaceMinLength)]
        public string Place { get; set; } = null !;

    }
}
