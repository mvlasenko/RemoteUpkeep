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
                    //create new target
                    Target target = new Target
                    {
                        ChangedDateTime = DateTime.Now,
                        Description = model.Description
                    };

                    //set region
                    if (model.RegionId != null)
                    {
                        target.RegionId = (int)model.RegionId;
                    }

                    //get images
                    if (!String.IsNullOrEmpty(model.Images))
                    {
                        foreach (string imageId in model.Images.Trim('|').Split('|'))
                        {
                            if (!String.IsNullOrEmpty(imageId))
                            {
                                Image image = context.Images.FirstOrDefault(x => x.Id == new Guid(imageId));
                                if (image != null)
                                    target.Images.Add(context.Images.Single(x => x.Id == new Guid(imageId)));
                            }
                        }
                    }

                    //create new order details
                    OrderDetails orderDetails = new OrderDetails
                    {
                        OrderStatus = OrderStatus.New,
                        Target = target
                    };

                    //get service
                    Service service = context.Services.FirstOrDefault(x => x.Id == model.ServiceId);
                    if (service != null)
                        orderDetails.Services.Add(context.Services.Single(x => x.Id == model.ServiceId));

                    //create new action
                    Models.Action action = new Models.Action
                    {
                        ChangedDateTime = DateTime.Now,
                        DueDate = model.DueDate,
                        Description = model.Description
                    };
                    orderDetails.Actions.Add(action);

                    //create new order
                    Order order = new Order
                    {
                        CreatedDateTime = DateTime.Now,
                        UserId = this.User.Identity.GetUserId()
                    };
                    order.OrderDetails.Add(orderDetails);

                    //save order
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