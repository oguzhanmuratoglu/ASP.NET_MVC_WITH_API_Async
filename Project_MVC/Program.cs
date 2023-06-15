using Autofac.Extensions.DependencyInjection;
using Autofac;
using Project_MVC.Modules;
using Microsoft.EntityFrameworkCore;
using Project_Repository;
using System.Reflection;
using Project_Service.Mapping;
using FluentValidation.AspNetCore;
using Project_Service.Validations;
using Project_MVC.Filters;
using Project_MVC.Services;
using Project_Core.UnitOfWork;
using Project_Repository.UnitOfWorks;
using Project_Core.Services;

namespace Project_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddFluentValidation
                (x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>()); ;

            builder.Services.AddScoped<IProductApiServices, ProductApiServices>();
            builder.Services.AddScoped<ICategoryApiServices, CategoryApiServices>();

          


            builder.Services.AddAutoMapper(typeof(MapProfile));


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.
            RegisterModule(new RepoServiceModule()));


            builder.Services.AddDbContext<ProjectDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConntection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(ProjectDbContext)).GetName().Name);
                });
            });

            builder.Services.AddHttpClient<ProductApiServices>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });

            builder.Services.AddHttpClient<CategoryApiServices>(opt =>
            {
                opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);
            });


            builder.Services.AddScoped(typeof(NotFoundFilter<>));


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