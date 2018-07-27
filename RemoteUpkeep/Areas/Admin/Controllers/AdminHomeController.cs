using System.Web.Mvc;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Users = "mark.vlasenko@gmail.com")] //todo: user type based security
    public class AdminHomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}