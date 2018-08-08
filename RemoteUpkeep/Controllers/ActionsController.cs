using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Controllers
{
    [Authorize(Roles = "admin,dealer")]
    public class ActionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var list = db.Actions.Include(a => a.OrderDetails).Include(a => a.ChangedBy).Where(x => x.AssignedUserId == userId).ToList();

            return View(list);
        }
    }
}