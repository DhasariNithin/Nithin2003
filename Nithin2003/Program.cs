using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Nithin2003.Database;
using Nithin2003.Interfaces;
using Nithin2003.Services;


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

           
            builder.Services.AddSession();
            builder.Services.AddDbContext<ApplicationData>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession(); // Enable session support


            builder.Services.AddScoped<IProductService,ProductService>(); // Register ProductService with DI container
            builder.Services.AddScoped<IUserService, UserService>();

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