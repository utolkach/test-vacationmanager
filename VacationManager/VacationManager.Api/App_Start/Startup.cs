using System;
using System.IO;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using VacationManager;
using VacationManager.Providers;

[assembly: OwinStartup(typeof(Startup))]
namespace VacationManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            ConfigureDataDirectory();
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            DiBootstrap.Initialize(config);
            app.UseWebApi(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }

        private static void ConfigureDataDirectory()
        {
            var codeBase = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            var newUri = new Uri(codeBase).AbsolutePath;
            var bin = Path.GetDirectoryName(newUri);
            if (string.IsNullOrEmpty(bin))
            {
                throw new Exception("Can't determine Bin folder");
            }

            var path = Path.Combine(bin, "DATA");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new AuthorizationServerProvider(),
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}