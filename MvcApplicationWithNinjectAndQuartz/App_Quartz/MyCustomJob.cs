using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Ninject.Extensions.Logging;

namespace MvcApplicationWithNinjectAndQuartz.App_Quartz
{
    public class MyCustomJob : IJob
    {
        ILogger _logger;
        public MyCustomJob(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(JobExecutionContext context)
        {
            _logger.Info("job executed");
        }
    }
}