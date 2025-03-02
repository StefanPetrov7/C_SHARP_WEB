using Microsoft.EntityFrameworkCore;

using CinemaApp.Data;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // App builder  
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string dbConnectionString = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.

            // Dependency Injection package need to be installed.

            // This is registering the context so we can access it with dependency injection
            builder.Services.AddDbContext<CinemaDbContext>(option =>
            {
                option.UseSqlServer(dbConnectionString);
            });
            builder.Services.AddControllersWithViews();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
