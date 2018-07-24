using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.User);
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            Order order = new Order();
            order.CreatedDateTime = DateTime.Now;

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                order.CreatedDateTime = DateTime.Now;

                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Edit", new { id = order.Id });
            }

            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders
                .Include(x => x.OrderDetails)
                .FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            foreach (OrderDetails details in order.OrderDetails)
            {
                details.ServiceIds = details.Services.Select(x => x.Id).ToList();
                details.Target.ImageIds = String.Join("|", details.Target.Images.Select(x => x.Id));
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                db.Configuration.ProxyCreationEnabled = false;

                db.Entry(order).State = EntityState.Modified;

                foreach (OrderDetails details in order.OrderDetails)
                {
                    db.Entry(details).State = EntityState.Modified;
                    db.Entry(details.Target).State = EntityState.Modified;

                    details.Target.ChangedDateTime = DateTime.Now;
                    details.Target.ChangedByUserId = this.User.Identity.GetUserId();
                }

                db.SaveChanges();

                //save many-to-many relation in separate transaction
                foreach (OrderDetails details in order.OrderDetails)
                {
                    OrderDetails originalDetails = db.OrderDetails
                        .Include(x => x.Services)
                        .Include(x => x.Target.Images)
                        .Single(x => x.Id == details.Id);

                    db.Entry(originalDetails).State = EntityState.Modified;

                    originalDetails.Services = details.ServiceIds == null ? 
                        new List<Service>() : 
                        details.ServiceIds.Select(serviceId => db.Services.FirstOrDefault(x => x.Id == serviceId)).ToList();

                    originalDetails.Target.Images = details.Target == null || details.Target.ImageIds == null ? 
                        new List<Image>() : 
                        details.Target.ImageIds.Trim('|').Split('|').Distinct().Select(imageId => db.Images.FirstOrDefault(x => x.Id == new Guid(imageId))).ToList();

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
