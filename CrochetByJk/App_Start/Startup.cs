using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace CrochetByJk
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")
            });

            GlobalConfiguration.Configuration.UseSqlServerStorage("Hangfire");
            appBuilder.UseHangfireDashboard();
            appBuilder.UseHangfireServer();
            HangfireConfig.Configure();
        }
    }
}