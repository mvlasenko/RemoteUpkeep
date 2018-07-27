using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Users = "mark.vlasenko@gmail.com")] //todo: user type based security
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(s => s.Country).Include(s => s.Region);
            return View(users.ToList());
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            ApplicationUser user = new ApplicationUser();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                //todo: send email

                user.Languages = user.LanguageIds.Select(languageId => db.Languages.FirstOrDefault(x => x.Id == languageId)).ToList();

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = db.Users.Find(model.Id);
                db.Entry(user).State = EntityState.Modified;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.CountryId = model.CountryId;
                user.RegionId = model.RegionId; //todo: more fields

                user.Languages = user.LanguageIds.Select(languageId => db.Languages.FirstOrDefault(x => x.Id == languageId)).ToList();

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser user = db.Users.Find(id);
            db.Users.Remove(user);
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