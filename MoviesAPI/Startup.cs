using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MoviesAPI.Interfaces;
using MoviesAPI.Services;
using AutoMapper;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using MoviesAPI.DB;

namespace MoviesAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAutoMapper(
                opt => opt.CreateMissingTypeMaps = true,
                Assembly.GetEntryAssembly());

            services.AddScoped<IMoviesService, MoviesService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<MoviesContext>();


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Movies API", });
                c.CustomSchemaIds(i => i.FullName);
                var basePath = System.AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "MoviesAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                var swaggerPath = "/swagger/v1/swagger.json";
                c.SwaggerEndpoint(swaggerPath, "Movies API V1");
            });
        }
    }
}
