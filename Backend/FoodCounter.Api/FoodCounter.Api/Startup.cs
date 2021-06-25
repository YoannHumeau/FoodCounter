using FoodCounter.Api.Services;
using FoodCounter.Api.Services.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using FoodCounter.Api.DataAccess.DataAccess;
using FoodCounter.Api.Exceptions;
using FoodCounter.Api.Repositories;
using FoodCounter.Api.Repositories.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;

//using FoodCounter.Api.Exceptions;

namespace FoodCounter.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IAlimentService, AlimentService>();
            services.AddScoped<IAlimentConsumeService, AlimentConsumeService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAlimentRepository, AlimentRepository>();
            services.AddScoped<IAlimentConsumeRepository, AlimentConsumeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // HttpContext Injection - Used for having User information in service classes
            services.AddHttpContextAccessor();

            // DbAccess injection
            services.AddScoped<DbAccess>(sp => new DbAccess(
                Configuration.GetConnectionString("DatabaseType"),
                Configuration.GetConnectionString("DatabaseConnectionString")));

            // Authentication configuration
            var jwtKey = Encoding.ASCII.GetBytes(Configuration.GetSection("Authentication:SecretJwtKey").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodCounter.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FoodCounter.Api v1"));
            }

            // Get exceptions from the project and format it the good way (Status code + errors message)
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exception = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;

                // In case of personal exception found, give the status code to return
                if (exception.GetType().Assembly.GetName().Name == typeof(Program).Assembly.GetName().Name)
                {
                    context.Response.StatusCode = (int) ((HttpStatusException)exception).StatusCode;
                }
                
                await context.Response.WriteAsJsonAsync( new { Errors = new  { Message = exception.Message }} );
            }));
            
//            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
