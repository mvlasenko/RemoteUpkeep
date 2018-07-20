using System.Linq;
using System.Web.Mvc;
using RemoteUpkeep.Properties;

namespace RemoteUpkeep.Controllers
{
    public class ResourcesController : Controller
    {
        public JsonResult Index()
        {
            return Json(
                 typeof(Resources)
                 .GetProperties()
                 .Where(p => p.Name !="ResourceManager")
                 .ToDictionary(p => p.Name, p => p.GetValue(null) as string), JsonRequestBehavior.AllowGet);
        }
    }
}