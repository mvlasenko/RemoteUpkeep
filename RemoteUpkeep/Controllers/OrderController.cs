using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {
            OrderViewModel model = new OrderViewModel();

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult StepOne(OrderViewModel model)
        {
            //save selected service
            this.Session.Add("OrderTmp", model);

            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("StepTwo");
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
                using (var context = new ApplicationDbContext())
                {
                    //create new order
                    Order order = new Order
                    {
                        Description = model.Description,
                        CreatedDateTime = DateTime.Now,
                        OrderType = OrderType.New,
                        UserId = this.User.Identity.GetUserId()
                    };

                    //set region
                    if (model.RegionId != null)
                    {
                        order.RegionId = (int)model.RegionId;
                    }

                    //get service
                    Service service = context.Services.FirstOrDefault(x => x.Id == model.ServiceId);
                    if (service != null)
                        order.Services.Add(context.Services.Single(x => x.Id == model.ServiceId));
                    
                    //get images
                    if (!String.IsNullOrEmpty(model.Images))
                    {
                        foreach (string imageId in model.Images.Trim('|').Split('|'))
                        {
                            if (!String.IsNullOrEmpty(imageId))
                            {
                                Image image = context.Images.FirstOrDefault(x => x.Id == new Guid(imageId));
                                if (image != null)
                                    order.Images.Add(context.Images.Single(x => x.Id == new Guid(imageId)));
                            }
                        }
                    }

                    //create new action
                    Models.Action action = new Models.Action
                    {
                        ChangedDateTime = DateTime.Now,
                        DueDate = model.DueDate,
                        Description = model.Description
                    };
                    order.Actions.Add(action);

                    //save target
                    context.Orders.Add(order);
                    context.SaveChanges();
                }

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