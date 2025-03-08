using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CinemaApp.Data;
using CinemaApp.Web.ViewModels.Cinema;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;

namespace CinemaApp.Web.Controllers
{
    public class CinemaController : BaseController
    {

        private readonly CinemaDbContext dbContext;

        public CinemaController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<CinemaIndexViewModel> cinemas = await dbContext.Cinemas
                   .Select(x => new CinemaIndexViewModel()
                   {
                       Id = x.Id.ToString(),
                       Name = x.Name,
                       Location = x.Location,
                   })
                   .OrderBy(x => x.Location)
                   .ToArrayAsync();

            return this.View(cinemas);

        }

        // this Get Create will the form which the user will use to input the data
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return this.View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(CinemaAddFormModel model)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            Cinema cinema = new Cinema()
            {
                Name = model.Name,
                Location = model.Location,
            };

            await dbContext.Cinemas.AddAsync(cinema);
            await dbContext.SaveChangesAsync();
            return this.RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {

            Guid cinemaGuid = Guid.Empty;

            bool isIdValid = this.IsGuidIdValid(id, ref cinemaGuid);

            if (isIdValid == false)
            {
                return RedirectToAction(nameof(Index));
            }

            Cinema? cinema = await dbContext.Cinemas
                .Include(x => x.CinemaMovies)
                .ThenInclude(x => x.Movie)
                .FirstOrDefaultAsync(x => x.Id == cinemaGuid);

            // Invalid GUID in the URL
            if (cinema == null)
            {
                return RedirectToAction(nameof(Index));
            }

            CinemaDetailsViewModel viewModel = new CinemaDetailsViewModel()
            {
                Name = cinema.Name,
                Location = cinema.Location,
                Movies = cinema.CinemaMovies
                .Where(x => x.IsDeleted == false)
                .Select(x => new CinemaMovieViewModel()
                {
                    Title = x.Movie.Title,
                    Duration = x.Movie.Duration,
                })
                .ToArray()
            };

            return this.View(viewModel);

        }


    }
}
