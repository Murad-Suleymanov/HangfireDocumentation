using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Hangfire.Startup))]
namespace Hangfire
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseHangfireServer();

            GlobalConfiguration.Configuration
                    .UseSqlServerStorage("Server=localhost;Database=EducationSite;Integrated Security=true");

            app.UseHangfireDashboard();
        }
    }
}
