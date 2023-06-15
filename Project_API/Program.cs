using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project_API.Filters;
using Project_API.Middlewares;
using Project_API.Modules;
using Project_Core.Repositories;
using Project_Core.UnitOfWork;
using Project_Repository;
using Project_Repository.Repositories;
using Project_Repository.UnitOfWorks;
using Project_Service.Mapping;
using Project_Service.Validations;
using System;
using System.Reflection;

namespace Project_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMemoryCache();


            builder.Services.AddScoped(typeof(NotFoundFilter<>));

            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
            //builder.Services.AddScoped<IProductRepository, ProductRepository>();
            //builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            //builder.Services.AddScoped<ICategoryService, CategoryService>();


            builder.Services.AddAutoMapper(typeof(MapProfile));






            builder.Services.AddDbContext<ProjectDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConntection"), option =>
                {
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(ProjectDbContext)).GetName().Name);
                });
            });


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.
            RegisterModule(new RepoServiceModule()));




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCustomException();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}