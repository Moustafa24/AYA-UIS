
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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Presistence.Identity;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Shared.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

namespace AYA_UIS
{
    public class Program
    {
        public static async Task Main(string[] args)
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

            #region Auth
            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
             var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;    

            }).AddJwtBearer(options=> {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))


                };
            
            });
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<IdentityAYADbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"))
           );

            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.User.RequireUniqueEmail = true;

            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityAYADbContext>()
            .AddDefaultTokenProviders(); 



            builder.Services.AddScoped<IDataSeeding, DataSeeding>();



            #endregion

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });
            builder.Services.AddAutoMapper(cfg => { }, typeof(AssemblyRefrenceServ).Assembly);
            builder.Services.AddScoped<IDepartmentFeeService, DepartmentFeeService>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddHttpContextAccessor();


            #region Auth In Swagger

                  builder.Services.AddSwaggerGen(option =>
                  {
                            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                            {
                                   In = ParameterLocation.Header,
                                   Description = "Please enter a valid token",
                                   Name = "Authorization",
                                   Type = SecuritySchemeType.Http,
                                   BearerFormat = "JWT",
                                   Scheme = "Bearer"
                            });
                        option.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                       Reference = new OpenApiReference
                                       { 
                                             Type=ReferenceType.SecurityScheme,
                                              Id="Bearer"
                                       }
                                },
                                     new string[]{}
                            }
                        });
                  });


            #endregion

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalExceptionHandlingMiddelWare>();

            var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await dataSeeder.SeedIdentityDataAsync();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.MapControllers();
            app.UseStaticFiles();
            app.Run();
        }
    }
}
