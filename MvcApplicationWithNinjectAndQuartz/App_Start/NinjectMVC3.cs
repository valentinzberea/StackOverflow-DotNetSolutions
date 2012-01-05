[assembly: WebActivator.PreApplicationStartMethod(typeof(MvcApplicationWithNinjectAndQuartz.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(MvcApplicationWithNinjectAndQuartz.App_Start.NinjectMVC3), "Stop")]

namespace MvcApplicationWithNinjectAndQuartz.App_Start
{
    using System.Reflection;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;
    using Quartz.Spi;
    using MvcApplicationWithNinjectAndQuartz.App_Quartz;
    using Quartz;
    using System;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IJobFactory>().To<NinjectJobFactory>();
            kernel.Bind<ISchedulerFactory>().ToProvider<QuartzSchedulerFactoryProvider>();
            kernel.Bind<IScheduler>().ToProvider<QuartzSchedulerProvider>().InSingletonScope()
                .OnActivation((IScheduler s) => s.Start());
            kernel.Bind<Func<Type, IJob>>().ToMethod(ctx => t => (IJob)kernel.Get(t));
        }
    }
}
