using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CinemaApp.Web.Infrastructure.Extensions;

using CinemaApp.Data;
using CinemaApp.Data.Models;

namespace CinemaApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // App builder  
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            string? dbConnectionString = builder.Configuration.GetConnectionString("SQLServer");

            // Add services to the container.

            // Dependency Injection package need to be installed.

            // This is registering the context so we can access it with dependency injection
            builder.Services.AddDbContext<CinemaDbContext>(option =>
            {
                option.UseSqlServer(dbConnectionString);
            });

            // Adding the settings for the Identity Data >> note that they have to be in the below specific order.  

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(cfg =>
            {
                ConfigureIdentity(builder, cfg);
            })
              .AddEntityFrameworkStores<CinemaDbContext>()
              .AddRoles<IdentityRole<Guid>>()
              .AddSignInManager<SignInManager<ApplicationUser>>()
              .AddUserManager<UserManager<ApplicationUser>>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

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

            // First we are adding Authentication 
            app.UseAuthentication();

            // Second we are adding the Authorization -> this can work only if the app knows who is the user (Authentication)
            app.UseAuthorization();

            // Below was not in the VIDEO >> this will load up directly the log in page
            //app.MapGet("/", context =>
            //{
            //    context.Response.Redirect("/Identity/Account/Login");
            //    return Task.CompletedTask;
            //});

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Creating routing table for all Razor pages. 
            app.MapRazorPages();

            // Our extension method to apply the migrations once the app is started. 

            app.ApplyMigrations();

            app.Run();
        }

        private static void ConfigureIdentity(WebApplicationBuilder builder, IdentityOptions cfg)
        {
            cfg.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
            cfg.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
            cfg.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
            cfg.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumerical");
            cfg.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequiredLength");
            cfg.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequiredUniqueCharacters");

            cfg.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
            cfg.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
            cfg.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

            cfg.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");

        }
    }
}
