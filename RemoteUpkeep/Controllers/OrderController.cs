using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.EmailEngine;
using RemoteUpkeep.Models;
using RemoteUpkeep.Properties;
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
                return RedirectToAction("Login", "Account", new { returnUrl = "/Order/StepTwo" });
            }
        }

        public ActionResult StepTwo()
        {
            OrderViewModel model = this.Session["OrderTmp"] as OrderViewModel;
            if (model == null)
                model = new OrderViewModel();

            model.HasGeography = true;

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
                        Geography = model.HasGeography ? model.Geography : null,
                        Description = model.Description
                    };

                    //set region
                    if (model.RegionId != null)
                    {
                        target.RegionId = (int)model.RegionId;
                    }

                    //get images
                    if (!String.IsNullOrEmpty(model.FileIds))
                    {
                        foreach (string imageId in model.FileIds.Trim('|').Split('|'))
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

                    string userId = this.User.Identity.GetUserId();
                    ApplicationUser user = context.Users.Find(userId);


                    //TODO: set UI culture!!!!

                    //get user language
                    int? primaryLanguageId = user.PrimaryLanguageId;
                    if (primaryLanguageId == null)
                    {
                        Language language = context.Languages.FirstOrDefault(x => x.Code == CultureInfo.CurrentUICulture.Name);
                        if (language != null)
                        {
                            primaryLanguageId = language.Id;
                        }
                    }

                    //new message
                    Message message = new Message
                    {
                        Subject = Resources.OrderCreatedSubject,
                        Text = Resources.OrderCreatedMessage,
                        Date = DateTime.Now,
                        MessageType = MessageType.Email, //todo
                        ReceiverId = userId,
                        LanguageId = primaryLanguageId
                    };
                    orderDetails.Messages.Add(message);

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

                    //send message
                    EmailHelper.SendEmail(user, message.Text, message.Subject); //todo check status
                }

                return PartialView("StepComplete", model);
            }
            else
            {
                //save selected service
                this.Session.Add("OrderTmp", model);

                return RedirectToAction("Login", "Account", new { returnUrl = "/Order/StepTwo" });
            }
        }
    }
}