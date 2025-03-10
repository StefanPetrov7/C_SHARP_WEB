using Microsoft.AspNetCore.Identity;

namespace CinemaApp.Data.Models
{
    // Extension of the default IdentityUser
    // IdentityUser<Guid> we are giving generic type of the primary key type, in our case GUID. 
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();   
        }

        public virtual ICollection<ApplicationUserMovie> ApplicationUserMovies { get; set; } = new HashSet<ApplicationUserMovie>();


    }
}
