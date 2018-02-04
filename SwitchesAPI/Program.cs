
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SwitchesAPI.DB;
using SwitchesAPI.DB.DbInitializer;

namespace SwitchesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var host = BuildWebHost(args);

            using ( var scope = host.Services.CreateScope() )
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<SwitchesContext>();
                    DbInitializer.Initialize(context);
                }
                catch ( Exception ex )
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
    
}
