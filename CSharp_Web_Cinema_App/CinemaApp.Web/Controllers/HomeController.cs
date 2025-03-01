using System.Diagnostics;
using CinemaApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CinemaApp.Web.Controllers
{
    public class HomeController : Controller
    {


        public HomeController()
        {

        }

        public IActionResult Index()
        {
            // 3 ways to passing information to the view from the controller
            // Using ViewData / ViewBag or passing the ViewModel. 

            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to the cinema Web App";
            return View();
        }

    }
}
