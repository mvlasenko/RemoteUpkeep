using System.Web.Mvc;
using RemoteUpkeep.Helpers;

namespace RemoteUpkeep.Controllers
{
    public class LanguagesController : BaseController
    {
        [HttpGet, ValidateInput(false)]
        public ActionResult SetCulture(int id)
        {
            TranslationHelper.SetCulture(id);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}