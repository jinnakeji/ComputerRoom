using AspectCore.DynamicProxy;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Logger
{
    public class LogInterceptorAttribute : AbstractInterceptorAttribute
    {
        private ILogger logger;

        public LogInterceptorAttribute(ILogger logger)
        {
            this.logger = logger;
        }

        public override Task Invoke(AspectContext context, AspectDelegate next)
        {
            logger.Info(LogType.系统跟踪, $"{context.ImplementationMethod.Name}--Begin\r\n" +
                $"params:{context.Parameters.ToJson()}");
            var task = next(context);
            logger.Info(LogType.系统跟踪, $"{context.ImplementationMethod.Name}--End");
            return task;
        }
    }
}
