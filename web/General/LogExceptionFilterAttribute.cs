using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Itoil
{
    public class LogExceptionFilterAttribute : ExceptionFilterAttribute
    {
        NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            logger.Error(actionExecutedContext.Exception);
        }
    }
}