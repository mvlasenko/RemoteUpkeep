using System.Web.Mvc;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}