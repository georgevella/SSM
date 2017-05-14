using System.IO;
using Glyde.AspNetCore.Startup;
using Microsoft.AspNetCore.Hosting;

namespace SiteSpeedManager.Agent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseGlydeBootstrappingForApi()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
