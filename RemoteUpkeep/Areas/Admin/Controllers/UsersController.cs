using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using RemoteUpkeep.EmailEngine;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Users = "mark.vlasenko@gmail.com")] //todo: user type based security
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = db.Users.Include(s => s.Country).Include(s => s.Region);
            return View(users.ToList());
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            UserViewModel model = new UserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone,
                    CountryId = model.CountryId,
                    RegionId = model.RegionId
                };
                user.Languages = model.LanguageIds == null ? new List<Language>() :
                    model.LanguageIds.Select(languageId => context.Languages.FirstOrDefault(x => x.Id == languageId)).ToList();

                var result = await UserManager.CreateAsync(user, "Qwerty@123");
                if (result.Succeeded)
                {
                    EmailHelper.SendConfirmEmail(user, Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = UserManager.GenerateEmailConfirmationToken(user.Id) }, protocol: Request.Url.Scheme));
                    return RedirectToAction("Index");
                }
            }

            return View(model);
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

            UserViewModel model = new UserViewModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Phone = user.PhoneNumber;
            model.CountryId = user.CountryId;
            model.RegionId = user.RegionId;
            model.LanguageIds = user.Languages.Select(x => x.Id).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                string userId = model.Id;

                var user = context.Users.Include(x => x.Languages).FirstOrDefault(x => x.Id == userId);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.CountryId = model.CountryId;
                user.RegionId = model.RegionId;
                user.Languages = model.LanguageIds == null ? new List<Language>() :
                    model.LanguageIds.Select(languageId => context.Languages.FirstOrDefault(x => x.Id == languageId)).ToList();

                context.Entry(user).State = EntityState.Modified;
                await context.SaveChangesAsync();

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