using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Workers.Web.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Workers.Infrastructure.Data.Context;

namespace Workers.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                //var services = scope.ServiceProvider;
                var context = scope.ServiceProvider.GetRequiredService<WorkerDbContext>();
                DataSeeder.SeedSystem(context);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
