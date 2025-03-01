using Microsoft.EntityFrameworkCore;
using EventMeData;
using EventMiServicesData;
using EventMiServicesData.Contracts;

namespace EventMeWebProject
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // TODO => View All functionality   

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("Default");  // taking information from app settings json file 

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EventMeDbContext>(cfg => cfg.UseSqlServer(connectionString));  // using dependency injection to add the DBContext = using MS Dependency Injection pkg.
            builder.Services.AddScoped<IEventService, EventService>();   // Register the new service 

            // Application pipe line strats from here:

            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Used for loading all CSS and JS files
            app.UseStaticFiles();  

            // For all Actions 
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using IServiceScope scope = app.Services.CreateScope();    // This is added to enable automatic migrations => all new migrations will be applied on each app rerun
            EventMeDbContext db = scope.ServiceProvider.GetRequiredService<EventMeDbContext>();
            await db.Database.MigrateAsync();

            await app.RunAsync();
        }
    }
}