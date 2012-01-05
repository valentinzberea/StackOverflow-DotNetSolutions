using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz.Spi;
using Quartz;
using Ninject.Activation;

namespace MvcApplicationWithNinjectAndQuartz.App_Quartz
{
    public class QuartzSchedulerProvider : Provider<IScheduler>
    {
        private readonly IJobFactory jobFactory;
        private readonly IEnumerable<ISchedulerListener> listeners;
        private readonly ISchedulerFactory schedulerFactory;

        public QuartzSchedulerProvider(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<ISchedulerListener> listeners)
        {
            this.jobFactory = jobFactory;
            this.listeners = listeners;
            this.schedulerFactory = schedulerFactory;
        }

        protected override IScheduler CreateInstance(IContext context)
        {
            var scheduler = this.schedulerFactory.GetScheduler();
            scheduler.JobFactory = this.jobFactory;
            foreach (var listener in this.listeners)
            {
                scheduler.AddSchedulerListener(listener);
            }

            return scheduler;
        }
    }
}