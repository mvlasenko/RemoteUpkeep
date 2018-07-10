using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Messages
        public ActionResult Index()
        {
            var messages = db.Messages.Include(m => m.Receiver).Include(m => m.Sender);
            return View(messages.ToList());
        }

        // GET: Admin/Messages/Create
        public ActionResult Create()
        {
            Message message = new Message();
            message.Date = DateTime.Now;
            message.SenderId = this.User.Identity.GetUserId();

            return View(message);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.Date = DateTime.Now;
                message.SenderId = this.User.Identity.GetUserId();

                //todo
                message.MessageType = MessageType.Email;
                //todo: send email

                db.Messages.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(message);
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
