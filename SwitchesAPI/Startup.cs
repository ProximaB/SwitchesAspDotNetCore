using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Services;
using AutoMapper;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using SwitchesAPI.DB;
using System.Web.Http.Cors;
using SwitchesAPI.Middlewares;

namespace SwitchesAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors();

            services.AddAutoMapper(
                opt => opt.CreateMissingTypeMaps = true,
                Assembly.GetEntryAssembly());

            services.AddScoped<ISwitchesService, SwitchesService>();
            services.AddScoped<IRoomsService, RoomsService>();
            services.AddScoped<SwitchesContext>();           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Switches API", });
                c.CustomSchemaIds(i => i.FullName);
                var basePath = System.AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "SwitchesAPI.xml");
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

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost")
                    .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            //for Client presentation
            app.UseDefaultFiles();
            app.UseStaticFiles();
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("SwitchAPIClient.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            //

            var wsOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024
            };
            app.UseWebSockets(wsOptions);
            app.UseWebSocketHandler();

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                var swaggerPath = "/swagger/v1/swagger.json";
                c.SwaggerEndpoint(swaggerPath, "Switches API V1");
            });

 
        }
    }
}
