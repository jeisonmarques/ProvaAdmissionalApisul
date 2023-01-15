using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProvaAdmissionalCSharpApisul.UI.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                            .AddCommandLine(args)
                            .AddEnvironmentVariables(prefix: "ASPNETCORE_")
                            .Build();

            var host = WebHost.CreateDefaultBuilder(args)
                               .UseConfiguration(config)
                               //.UseKestrel()
                               .UseContentRoot(Directory.GetCurrentDirectory())
                               .UseIISIntegration()
                               .UseStartup<Startup>()
                               .Build();

            return host;
        }
    }
}