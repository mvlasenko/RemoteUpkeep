using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            OrderViewModel model = new OrderViewModel();

            using (var db = new ApplicationDbContext())
            {
                model.Services = db.Services.ToList();
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult StepOne(OrderViewModel model)
        {
            //todo: save order serviceId
            
            //step one - save selected service
            return PartialView("StepTwo", model);
        }
    }
}