using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet] // Action to run with GET request from Index 
        public IActionResult Index()
        {
            IEnumerable<Movie> allMovies = this.dbContext.Movies.ToList();

            return this.View(allMovies);  // this obj will be pass to Views >> Movie >> Index
        }

        [HttpGet]  // This action need to return the form where data will be inserted
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(AddMovieInputModel inputModel)
        {

            bool isReleasedDateValid = DateTime.TryParseExact(inputModel.ReleaseDate, ReleaseDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime releaseDate);

            if (!isReleasedDateValid)
            {
                // Will return the same view "Create" with the same wrong model data!
                this.ModelState.AddModelError(nameof(inputModel.ReleaseDate), String.Format("The Release Date is not in correct format {0}.", ReleaseDateFormat));
                return View(inputModel);
            }

            if (!this.ModelState.IsValid)
            {
                // Will return the same view "Create" with the same wrong model data!
                return View(inputModel);
            }

            Movie validatedMovie = new Movie()
            {
                Title = inputModel.Title,
                Genre = inputModel.Genre,
                ReleaseDate = releaseDate,
                Director = inputModel.Director,
                Duration = inputModel.Duration,
                Description = inputModel.Description,
            };

            this.dbContext.Movies.Add(validatedMovie);
            this.dbContext.SaveChanges();
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]  // This action need to return the form where data will be inserted
        public IActionResult Details(string id)
        {
            bool isValid = Guid.TryParse(id, out Guid guidId);

            if (!isValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = this.dbContext.Movies.FirstOrDefault(x => x.Id == guidId);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }
    }
}
