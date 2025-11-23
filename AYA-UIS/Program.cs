
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Presistence.Data;
using Presistence.Repositories;
using Services.Abstraction.Contracts;
using Services.Implementatios;
using Services;
using Services.Implementations;
using AYA_UIS.MiddelWares;
using Microsoft.AspNetCore.Mvc;
using AYA_UIS.Factories;

namespace AYA_UIS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AYA_UIS_InfoDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("InfoConnection"))
           );

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            builder.Services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrenceServ).Assembly);
            builder.Services.AddScoped<IDepartmentFeeService, DepartmentFeeService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpContextAccessor();
            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalExceptionHandlingMiddelWare>();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
