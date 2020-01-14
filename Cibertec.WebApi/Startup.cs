using System;
using System.Threading.Tasks;
using System.Web.Http;
using Cibertec.WebApi.App_Start;
using Microsoft.Owin;
using log4net;
using Owin;
using System.Web.Http.ExceptionHandling;
using Cibertec.WebApi.Handlers;

[assembly: OwinStartup(typeof(Cibertec.WebApi.Startup))]

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(typeof(Startup));
            log.Debug("Loggin habilitado");

            var config = new HttpConfiguration();
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            DIConfig.ConfigureInjector(config);
            TokenConfig.ConfigureOAuth(app, config);
            RouteConfig.Register(config);
            WebApiConfig.Configure(config);
            app.UseWebApi(config);
        }
    }
}
