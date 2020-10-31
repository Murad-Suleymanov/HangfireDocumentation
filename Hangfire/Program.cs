using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Topshelf;

namespace Hangfire
{
    class Program
    {
        //[STAThread]
        static void Main(string[] args)
        {
            //HostFactory.Run(config =>
            //{
            //    config.Service<Bootstrap>(service =>
            //    {
            //        service.ConstructUsing(s => new Bootstrap());
            //        service.WhenStarted(s => s.Start());
            //        service.WhenStopped(s => s.Stop());
            //    });
            //    config.RunAsLocalSystem();
            //    config.SetDescription("Hangfire as windows Service for DataCrawling Project");
            //    config.SetDisplayName("Hangfire Service Custom");
            //});
            HideConsoleWindow();

            GlobalConfiguration.Configuration
                .UseSqlServerStorage("Server=localhost;Database=EducationSite;Integrated Security=true");
            BackgroundJob.Enqueue(() => ProcessData("process this"));

            StartOptions options = new StartOptions();
            options.Urls.Add($"http://localhost:9095");
            //options.Urls.Add($"http://127.0.0.1:9095");
            WebApp.Start<Startup>(options);

            using (var server = new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate("new-task",
                    (() => ProcessData("hangfire")),
                    Cron.Minutely());
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }

        public static void ProcessData(string data)
        {
            Console.WriteLine(data);
        }

        private static void HideConsoleWindow()
        {
            var handle = GetConsoleWindow();

            ShowWindow(handle, 0);
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
