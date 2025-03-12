using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class BaseController : Controller
    {

        protected bool IsGuidIdValid(string? id, ref Guid parsedGuid)
        {
            // None-existing parameter in the URL
            if (String.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            bool isGuidValid = Guid.TryParse(id, out parsedGuid);

            // Invalid parameter in the URL
            if (isGuidValid == false)
            {
                return false;
            }

            return true;

        }

    }
}
