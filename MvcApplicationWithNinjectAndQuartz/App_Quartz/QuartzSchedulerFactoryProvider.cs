using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Ninject.Activation;
using System.Collections.Specialized;
using Quartz.Impl;

namespace MvcApplicationWithNinjectAndQuartz.App_Quartz
{
    public class QuartzSchedulerFactoryProvider : Provider<ISchedulerFactory>
    {
        protected override ISchedulerFactory CreateInstance(IContext context)
        {
            //get the default scheduler factory
            return new StdSchedulerFactory();
        }
    }
}