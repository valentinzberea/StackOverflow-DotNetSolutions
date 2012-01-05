using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz.Spi;
using Quartz;

namespace MvcApplicationWithNinjectAndQuartz.App_Quartz
{
    public class NinjectJobFactory : IJobFactory
    {
        private readonly Func<Type, IJob> jobFactory;

        public NinjectJobFactory(Func<Type, IJob> jobFactory)
        {
            this.jobFactory = jobFactory;
        }

        public IJob NewJob(TriggerFiredBundle bundle)
        {
            return this.jobFactory(bundle.JobDetail.JobType);
        }
    }
}