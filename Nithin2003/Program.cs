using Microsoft.EntityFrameworkCore;
using Nithin2003.Database;
using Nithin2003.Services;
using Nithin2003.Services.Interfaces;


namespace Nithin2003
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Adding services for WeatherForecastService
            builder.Services.AddHttpClient<IWeatherForecastService, WeatherForecastService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7081"); 
            });

            builder.Services.AddSession();
            builder.Services.AddDbContext<ApplicationData>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



            var app = builder.Build();

            builder.Logging.AddConsole();

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
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }


}