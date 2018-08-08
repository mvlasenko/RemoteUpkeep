using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.EmailEngine;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RemoteUpkeep.Properties;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Receiver).Include(m => m.Sender).Include(m => m.OrderDetails.Order);
            return View(messages.ToList());
        }

        // GET: Admin/Messages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = db.Messages.Find(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Admin/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult CreatePartial(int id)
        {
            Message message = new Message();
            message.OrderDetailsId = id;

            return PartialView("_CreatePartial", message);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreatePartial(Message model)
        {
            model.Date = DateTime.Now;
            model.SenderId = this.User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Messages.Add(model);
                db.SaveChanges();

                ApplicationUser sender = db.Users.Find(model.SenderId);
                ApplicationUser receiver = db.Users.Find(model.ReceiverId);
                EmailViewModel emailModel = new EmailViewModel { Sender = sender, Receiver = receiver, Body = model.Text, SignatureName = Resources.SiteName };

                EmailHelper.SendEmail(emailModel, model.Subject);
                //todo: save status

                var list = db.Messages.ToList();
                return Json(new { totalCount = list.Count });
            }

            return Json(new { success = false });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult ViewPartial(int id)
        {
            Message message = db.Messages.Find(id);

            EmailViewModel emailModel = new EmailViewModel { Sender = message.Sender, Receiver = message.Receiver, Body = message.Text, SignatureName = Resources.SiteName };

            string formattedBody = EmailHelper.GetFormattedBody(emailModel);
            message.Text = EmailHelper.GetHtmlBody(formattedBody, message.Subject);//, "/Content/Images/logo_email.png");

            return PartialView("_ViewPartial", message);
        }

        [HttpGet]
        public JsonResult GetListByDetails(int id)
        {
            var list = db.Messages.Include(m => m.Receiver).Include(m => m.Sender).Where(x => x.OrderDetailsId == id).ToList();
            foreach (Message item in list)
            {
                item.Subject = item.Subject.Cut(50);
                item.Text = item.Text.Cut(50);
            }

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
