﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using EventMiViewModels.Home;

namespace EventMeWebProject.Controllers
{
    public class HomeController : Controller
    {
      

        public HomeController()
        {
      
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
