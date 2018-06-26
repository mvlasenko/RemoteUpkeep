using System.Web.Mvc;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (var context = new ApplicationDbContext())
            {
                var orders = context.Orders;
            }

            return View();
        }
    }
}