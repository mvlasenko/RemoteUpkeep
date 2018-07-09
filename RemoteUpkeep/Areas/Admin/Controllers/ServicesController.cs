using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    public class ServicesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Services
        public ActionResult Index()
        {
            var services = db.Services.Include(s => s.CreatedBy);
            return View(services.ToList());
        }

        // GET: Admin/Services/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                service.CreatedDateTime = DateTime.Now;
                service.CreatedByUserId = this.User.Identity.GetUserId();

                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Admin/Services/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                service.CreatedDateTime = DateTime.Now;
                service.CreatedByUserId = this.User.Identity.GetUserId();

                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(service);
        }

        // GET: Admin/Services/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        // POST: Admin/Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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
