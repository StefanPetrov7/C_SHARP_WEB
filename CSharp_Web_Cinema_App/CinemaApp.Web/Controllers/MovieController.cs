using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Movie;
using static CinemaApp.Common.EntityValidationConstants.Movie;

namespace CinemaApp.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly CinemaDbContext dbContext;

        public MovieController(CinemaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet] // Action to run with GET request from Index 
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> allMovies = await this.dbContext.Movies.ToArrayAsync();

            return this.View(allMovies);  // this obj will be pass to Views >> Movie >> Index
        }

        [HttpGet]  // This action need to return the form where data will be inserted
        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieInputModel inputModel)
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
                ImageUrl = inputModel.ImageUrl, 
            };

            await this.dbContext.Movies.AddAsync(validatedMovie);
            await this.dbContext.SaveChangesAsync();
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]  // This action need to return the form where data will be inserted
        public async Task<IActionResult> Details(string? id)
        {
            Guid guidId = Guid.Empty;

            bool isGuidValid = this.IsGuidIdValid(id, ref guidId);

            if (!isGuidValid)
            {
                return RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == guidId);

            if (movie == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return this.View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> AddToProgram(string? id)
        {
            Guid movieGuid = Guid.Empty;

            bool isGuidValid = this.IsGuidIdValid(id, ref movieGuid);

            if (!isGuidValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == movieGuid);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            AddMovieToCinemaInputModel viewModel = new AddMovieToCinemaInputModel()
            {
                MovieId = movie.Id.ToString(),
                MovieTitle = movie.Title,
                Cinemas = await this.dbContext.Cinemas
                .Include(x => x.CinemaMovies)
                .ThenInclude(x => x.Movie)
                .Select(x => new CinemaCheckBoxItemInputModel()
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Location = x.Location,
                    IsSelected = x.CinemaMovies.Any(x => x.Movie.Id == movieGuid)
                })
                .ToArrayAsync()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToProgram(AddMovieToCinemaInputModel model)
        {

            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            Guid movieGuid = Guid.Empty;

            bool isGuidValid = this.IsGuidIdValid(model.MovieId, ref movieGuid);

            if (isGuidValid == false)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == movieGuid);

            if (movie == null)
            {
                return this.RedirectToAction(nameof(Index));
            }

            ICollection<CinemaMovie> entitiesToAdd = new HashSet<CinemaMovie>();

            foreach (CinemaCheckBoxItemInputModel cinemaInputModel in model.Cinemas)
            {
                Guid cinemaGuid = Guid.Empty;

                bool isCinemaGuidValid = this.IsGuidIdValid(cinemaInputModel.Id, ref cinemaGuid);

                if (isCinemaGuidValid == false)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema");
                    return this.View(model);
                }

                Cinema? cinema = await this.dbContext.Cinemas.FirstOrDefaultAsync(x => x.Id == cinemaGuid);

                if (cinema == null)
                {
                    this.ModelState.AddModelError(string.Empty, "Invalid cinema");
                    return this.View(model);
                }

                CinemaMovie? cinemaMovie = await this.dbContext.CinemaMovies.FirstOrDefaultAsync(x => x.MovieId == movieGuid && x.CinemaId == cinemaGuid);

                if (cinemaInputModel.IsSelected == true)
                {
                    if (cinemaMovie == null)
                    {
                        entitiesToAdd.Add(new CinemaMovie()
                        {
                            Cinema = cinema,
                            Movie = movie,
                        });

                    }
                    else
                    {
                        cinemaMovie.IsDeleted = false;   
                    }
                }
                else
                {
                    if (cinemaMovie != null) 
                    {
                        cinemaMovie.IsDeleted = true;
                    }
                }

                await this.dbContext.SaveChangesAsync();

            }

            await this.dbContext.CinemaMovies.AddRangeAsync(entitiesToAdd);
            await this.dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index), "Cinema");

        }
    }
}
