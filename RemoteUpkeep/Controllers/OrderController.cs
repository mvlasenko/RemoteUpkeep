using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

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
            //save selected service
            this.Session.Add("OrderTmp", model);

            if (this.User.Identity.IsAuthenticated)
            {
                return PartialView("StepTwo");
            }
            else
            {
                return RedirectToAction("Login", "Account", new { ReturnUrl = "/Order/StepTwo" });
            }
        }

        public ActionResult StepTwo()
        {
            OrderViewModel model = this.Session["OrderTmp"] as OrderViewModel;
            if (model == null)
                model = new OrderViewModel();

            return View("StepTwo", model);
        }

        [HttpPost]
        public ActionResult StepTwo(OrderViewModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                //save order


                return PartialView("StepComplete", model);
            }
            else
            {
                //save selected service
                this.Session.Add("OrderTmp", model);

                return RedirectToAction("Login", "Account", new { ReturnUrl = "/Order/StepTwo" });
            }
        }
    }
}