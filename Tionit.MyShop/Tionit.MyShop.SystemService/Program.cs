using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Tionit.ShopOnline.SystemService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isService = true;

#if DEBUG
            isService = false;
#endif

            if (isService)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                Directory.SetCurrentDirectory(pathToContentRoot);
            }

            var builder = CreateWebHostBuilder(
                args.Where(arg => arg != "--console").ToArray());
            var host = builder.Build();

            if (isService)
                host.RunAsService();
            else
                host.Run();
        }


        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddEventLog();
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                //configure the app here.
            })
            .UseStartup<Startup>()
            .UseUrls("http://0.0.0.0");
    }
}
