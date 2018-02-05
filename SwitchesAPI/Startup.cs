using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Services;
using System.Reflection;
using System.IO;
using AutoMapper;
using SwitchesAPI.DB;
using SwitchesAPI.Extensions.WebSocketManager;
using SwitchesAPI.Handlers.WebSocketsHandlers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Swashbuckle.AspNetCore.Swagger;
using SwitchesAPI.Middleware;

namespace SwitchesAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //potrzebny przy SSE, aktualnie nie dodawany domyślnie

            services.AddMvc();

            services.AddCors();

            services.AddAutoMapper(
                opt => opt.CreateMissingTypeMaps = true,
                Assembly.GetEntryAssembly());

            services.AddScoped<ISwitchesService, SwitchesService>();
            services.AddScoped<IRoomsService, RoomsService>();
            services.AddScoped<IUsersService, UsersService>();

            services.AddScoped<SwitchesContext>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Version = "v1", Title = "Switches API", });
                c.CustomSchemaIds(i => i.FullName);
                var basePath = System.AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "SwitchesAPI.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddWebSocketManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serv)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://192.168.0.101")
                    .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            );

            //for Client presentation only
            var options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("SwitchAPIClient.html");
            app.UseDefaultFiles(options);
            app.UseStaticFiles();
            //

            app.UseMiddleware<AuthenticationMiddleware>();


            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.ToString().Equals("/sse"))
            //    {
            //        var response = context.Response;
            //        response.Headers.Add("Content-Type", "text/event-stream");

            //        for (var i = 0; true; ++i)
            //        {
            //            // WriteAsync requires `using Microsoft.AspNetCore.Http`
            //            await response
            //                .WriteAsync($"data: Middleware {i} at {DateTime.Now}\r\r");

            //            response.Body.Flush();
            //            await Task.Delay(5 * 1000);
            //        }
            //    }

            //    await next.Invoke();
            //});

            app.UseWebSockets();

            //app.MapWebSocketManager("/test", serv.GetService<BoardNotificationsHandler>());
            app.MapWebSocketManager("/notifications", serv.GetService<NotificationsMessageHandler>());
            app.MapWebSocketManager("/SwitchChanged", serv.GetService<SwitchChangedHandler>());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                var swaggerPath = "/swagger/v1/swagger.json";
                c.SwaggerEndpoint(swaggerPath, "Switches API V1");
            });

            app.UseMvc();
        }
    }
}
