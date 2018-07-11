using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    public class ActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Actions
        public ActionResult Index()
        {
            var actions = db.Actions.Include(a => a.AssignedUser).Include(a => a.ChangedBy).Include(a => a.Order);
            return View(actions.ToList());
        }

        // GET: Admin/Actions/Create
        public ActionResult Create()
        {
            Models.Action action = new Models.Action();
            action.ChangedDateTime = DateTime.Now;
            action.ChangedByUserId = this.User.Identity.GetUserId();

            return View(action);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Action action)
        {
            if (ModelState.IsValid)
            {
                action.ChangedDateTime = DateTime.Now;
                action.ChangedByUserId = this.User.Identity.GetUserId();

                db.Actions.Add(action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(action);
        }

        // GET: Admin/Actions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }

            return View(action);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.Action action)
        {
            if (ModelState.IsValid)
            {
                action.ChangedDateTime = DateTime.Now;
                action.ChangedByUserId = this.User.Identity.GetUserId();

                db.Entry(action).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(action);
        }

        // GET: Admin/Actions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.Action action = db.Actions.Find(id);
            if (action == null)
            {
                return HttpNotFound();
            }
            return View(action);
        }

        // POST: Admin/Actions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Models.Action action = db.Actions.Find(id);
            db.Actions.Remove(action);
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
