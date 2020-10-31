using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangfire
{
    class Bootstrap
    {
        private IDisposable _host;


        public void Start()
        {
            var options = new StartOptions { Port = 8999 };
            _host = WebApp.Start<Startup>(options);
            Console.WriteLine("Hangfire has started");
            Console.WriteLine("Dashboard is available at http://localhost:8999/hangfire");
            Console.WriteLine();

            using (var server = new BackgroundJobServer())
            {
                RecurringJob.AddOrUpdate("new-task",
                    (() => Console.WriteLine("hangfire")),
                    Cron.Minutely());
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }

        public void Stop()
        {
            _host.Dispose();
        }
    }
}
