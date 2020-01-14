using Cibertec.Repositories.Dapper.NorthWind;
using Cibertec.UnitOfWork;
using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cibertec.WebApi.App_Start
{
    public class DIConfig
    {
        public static void ConfigureInjector(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IUnitOfWork>(() => new NorthwindUnitOfWork(
                ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString()));
            
            /*Registro del Log4Net*/
            container.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).
                MakeGenericType(c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);

            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        public sealed class Log4NetAdapter<T> : LogImpl
        {
            public Log4NetAdapter() : base(LogManager.GetLogger(typeof(T)).Logger) { }
        }
    }
}