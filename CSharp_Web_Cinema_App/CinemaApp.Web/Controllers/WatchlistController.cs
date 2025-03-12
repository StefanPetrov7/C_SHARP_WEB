using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using CinemaApp.Data;
using CinemaApp.Data.Models;
using CinemaApp.Web.ViewModels.Watchlist;

using static CinemaApp.Common.EntityValidationConstants.Movie;
using Microsoft.AspNetCore.Authorization;


namespace CinemaApp.Web.Controllers
{
    [Authorize] // If not Authorized (userId == null) will redirect to the login Index
    public class WatchlistController : BaseController
    {
        private readonly CinemaDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public WatchlistController(CinemaDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = this.userManager.GetUserId(User)!;

            IEnumerable<ApplicationUserWatchlistViewModel> watchlistViewModels = await this.dbContext.UsersMovies.Where(x => x.ApplicationUser.Id.ToString().ToLower() == userId.ToLower())
                .Include(x => x.Movie)
                .Select(x => new ApplicationUserWatchlistViewModel
                {
                    MovieId = x.MovieId.ToString(),
                    Title = x.Movie.Title,
                    Genre = x.Movie.Genre,
                    ReleaseDate = x.Movie.ReleaseDate.ToString(ReleaseDateFormat),
                    ImageUrl = x.Movie.ImageUrl!,
                })
                .ToListAsync();

            return View(watchlistViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWatchlist(string? movieId)
        {
            Guid movieGuid = Guid.Empty;

            if (IsGuidIdValid(movieId, ref movieGuid) == false)
            {
                return RedirectToAction("Index", "Movie");
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == movieGuid);

            if (movie == null)
            {
                return RedirectToAction("Index", "Movie");
            }

            Guid userGuid = Guid.Parse(this.userManager.GetUserId(User)!);

            bool addedToWatchlistAlready = await dbContext.UsersMovies
                .AnyAsync(x => x.ApplicationUserId == userGuid && x.MovieId == movieGuid);

            if (addedToWatchlistAlready == false)
            {
                ApplicationUserMovie userMovie = new ApplicationUserMovie()
                {
                    MovieId = movieGuid,
                    ApplicationUserId = userGuid,
                };

                await this.dbContext.UsersMovies.AddAsync(userMovie);
                await dbContext.SaveChangesAsync();

            }

            return this.RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWatchlist(string movieId)
        {
            Guid movieGuid = Guid.Empty;

            if (IsGuidIdValid(movieId, ref movieGuid) == false)
            {
                return RedirectToAction("Index", "Movie");
            }

            Movie? movie = await this.dbContext.Movies.FirstOrDefaultAsync(x => x.Id == movieGuid);

            if (movie == null)
            {
                return RedirectToAction("Index", "Movie");
            }

            Guid userGuid = Guid.Parse(this.userManager.GetUserId(User)!);

            ApplicationUserMovie? existingInWatchlist = await dbContext.UsersMovies
                .FirstOrDefaultAsync(x => x.ApplicationUserId == userGuid && x.MovieId == movieGuid);

            if (existingInWatchlist != null)
            {
                this.dbContext.UsersMovies.Remove(existingInWatchlist);
                await dbContext.SaveChangesAsync();
            }

            return this.RedirectToAction(nameof(Index));

        }
    }
}
