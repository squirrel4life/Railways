using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Configuration;

namespace Railways
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {                        
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/login")
            });
        
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            var enableFacebookAuth = bool.TryParse(ConfigurationManager.AppSettings["EnableFacebookAuth"], out bool fbEnabled) && fbEnabled;
            var enableGoogleAuth = bool.TryParse(ConfigurationManager.AppSettings["EnableGoogleAuth"], out bool googleEnabled) && googleEnabled;

            if (enableFacebookAuth)
            {
                app.UseFacebookAuthentication(
                    appId: ConfigurationManager.AppSettings["FacebookAppId"],
                    appSecret: ConfigurationManager.AppSettings["FacebookAppSecret"]);
            }

            if (enableGoogleAuth)
            {
                app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
                {
                    ClientId = ConfigurationManager.AppSettings["GoogleClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"]
                });
            }
        }
    }
}