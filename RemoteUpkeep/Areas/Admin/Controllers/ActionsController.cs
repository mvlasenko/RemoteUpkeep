using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Users = "mark.vlasenko@gmail.com")] //todo: user type based security
    public class ActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Actions/Create
        public ActionResult Create()
        {
            Models.Action action = new Models.Action();
            return View(action);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Action model)
        {
            model.ChangedDateTime = DateTime.Now;
            model.ChangedByUserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Actions.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
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
        public ActionResult Edit(Models.Action model)
        {
            model.ChangedDateTime = DateTime.Now;
            model.ChangedByUserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
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

        [HttpGet, ValidateInput(false)]
        public ActionResult CreatePartial(int id)
        {
            Models.Action action = new Models.Action();
            action.OrderDetailsId = id;

            return PartialView("_CreatePartial", action);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreatePartial(Models.Action model)
        {
            model.ChangedDateTime = DateTime.Now;
            model.ChangedByUserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Actions.Add(model);
                db.SaveChanges();

                var list = db.Actions.ToList();

                return Json(new { totalCount = list.Count });
            }

            return Json(new { success = false });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult UpdatePartial(int id)
        {
            Models.Action action = db.Actions.Find(id);
            return PartialView("_EditPartial", action);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(Models.Action model)
        {
            model.ChangedDateTime = DateTime.Now;
            model.ChangedByUserId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult DeletePartial(int id)
        {
            Models.Action action = db.Actions.Find(id);
            db.Actions.Remove(action);
            db.SaveChanges();

            return Json(new { detailsId = action.OrderDetailsId }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListByDetails(int id)
        {
            var list = db.Actions.Include(a => a.AssignedUser).Include(a => a.ChangedBy).Where(x=> x.OrderDetailsId == id).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
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
