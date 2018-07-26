using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.EmailEngine;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
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

                //todo: send email
                ApplicationUser sender = db.Users.Find(model.SenderId);
                ApplicationUser receiver = db.Users.Find(model.ReceiverId);
                EmailHelper.SendEmail(new EmailViewModel { Sender = sender, Receiver = receiver }, model.Text, "Email Message");

                var list = db.Messages.ToList();
                return Json(new { totalCount = list.Count });
            }

            return Json(new { success = false });
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult ViewPartial(int id)
        {
            Message message = db.Messages.Find(id);

            string body = EmailHelper.GetHtmlBody(new EmailViewModel { Sender = message.Sender, Receiver = message.Receiver }, message.Text, "Email Message", "/Content/Images/logo_email.png");
            message.Text = body;

            return PartialView("_ViewPartial", message);
        }

        [HttpGet]
        public JsonResult GetListByDetails(int id)
        {
            var list = db.Messages.Include(m => m.Receiver).Include(m => m.Sender).Where(x => x.OrderDetailsId == id).ToList();
            foreach (Message item in list)
            {
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
