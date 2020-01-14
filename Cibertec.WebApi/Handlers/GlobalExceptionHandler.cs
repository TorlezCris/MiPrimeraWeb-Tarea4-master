using log4net;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Cibertec.WebApi.Handlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        private readonly ILog log = LogManager.GetLogger(typeof(GlobalExceptionHandler));

        public override void Handle(ExceptionHandlerContext context)
        {
            log.Error(context.Exception);
            context.Result = new InternalServerErrorResult(context.Request);
        }
    }
}