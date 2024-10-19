using Assignment_WDA.Entities;
using Assignment_WDA.Interface;
using Assignment_WDA.Service;
using Microsoft.EntityFrameworkCore;

namespace Assignment_WDA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("Exam");
            builder.Services.AddDbContext<DataContext>(
                options => options.UseSqlServer(connectionString));
            builder.Services.AddScoped<IDepartmentRepository, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeService>();


            var app = builder.Build();

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