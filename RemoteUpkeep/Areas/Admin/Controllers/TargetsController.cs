using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    public class TargetsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Targets
        public ActionResult Index()
        {
            var targets = db.Targets.Include(t => t.ChangedBy).Include(t => t.Region);
            return View(targets.ToList());
        }

        // GET: Admin/Targets/Create
        public ActionResult Create()
        {
            Target target = new Target();
            target.ChangedDateTime = DateTime.Now;
            target.ChangedByUserId = this.User.Identity.GetUserId();

            return View(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Target target)
        {
            if (ModelState.IsValid)
            {
                target.ChangedDateTime = DateTime.Now;
                target.ChangedByUserId = this.User.Identity.GetUserId();

                db.Targets.Add(target);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(target);
        }

        // GET: Admin/Targets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target target = db.Targets.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }

            return View(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Target target)
        {
            if (ModelState.IsValid)
            {
                target.ChangedDateTime = DateTime.Now;
                target.ChangedByUserId = this.User.Identity.GetUserId();

                db.Entry(target).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(target);
        }

        // GET: Admin/Targets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Target target = db.Targets.Find(id);
            if (target == null)
            {
                return HttpNotFound();
            }
            return View(target);
        }

        // POST: Admin/Targets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Target target = db.Targets.Find(id);
            db.Targets.Remove(target);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult CreatePartial(int id)
        {
            Target target = new Target();
            target.ChangedDateTime = DateTime.Now;
            target.ChangedByUserId = this.User.Identity.GetUserId();
            target.OrderId = id;

            return PartialView("_CreatePartial", target);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreatePartial(Target model)
        {
            if (ModelState.IsValid)
            {
                //get images
                if (!String.IsNullOrEmpty(model.FileIds))
                {
                    foreach (string imageId in model.FileIds.Trim('|').Split('|'))
                    {
                        if (!String.IsNullOrEmpty(imageId))
                        {
                            Image image = db.Images.FirstOrDefault(x => x.Id == new Guid(imageId));
                            if (image != null)
                                model.Images.Add(db.Images.Single(x => x.Id == new Guid(imageId)));
                        }
                    }
                }

                model.ChangedDateTime = DateTime.Now;
                model.ChangedByUserId = this.User.Identity.GetUserId();
                db.Targets.Add(model);

                OrderDetails orderDetails = new OrderDetails();
                orderDetails.Target = model;
                orderDetails.OrderId = model.OrderId;
                orderDetails.OrderStatus = OrderStatus.New;
                db.OrderDetails.Add(orderDetails);

                db.SaveChanges();

                var list = db.OrderDetails.Where(x => x.OrderId == model.OrderId).ToList();

                return Json(new { totalCount = list.Count });
            }

            return Json(new { success = false });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult GetDetails(int id)
        {
            var model = db.OrderDetails.Where(x => x.OrderId == id).ToList();

            foreach (OrderDetails details in model)
            {
                details.ServiceIds = details.Services.Select(x => x.Id).ToList();
                details.Target.ImageIds = String.Join("|", details.Target.Images.Select(x => x.Id));
            }

            return PartialView("_DetailsListPartial", model);
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
