using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quartz;
using MvcApplicationWithNinjectAndQuartz.App_Quartz;

namespace MvcApplicationWithNinjectAndQuartz.Controllers
{
    public class HomeController : Controller
    {
        IScheduler _scheduler;
        public HomeController(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult StartJob()
        {
            _scheduler.ScheduleJob(
                new JobDetail("job", typeof(MyCustomJob)),
                new SimpleTrigger("test", 3, TimeSpan.FromSeconds(3)));
            return RedirectToAction("Index");
        }

    }
}
