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
