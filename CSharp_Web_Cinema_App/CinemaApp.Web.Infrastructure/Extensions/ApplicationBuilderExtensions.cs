using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CinemaApp.Data;

namespace CinemaApp.Web.Infrastructure.Extensions
{
    // Extension method for applying the migrations 
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app) 
        {

            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            CinemaDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<CinemaDbContext>()!;
            dbContext.Database.Migrate();

            return app;

        }

    }
}
