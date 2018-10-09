using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Areas.Admin.Controllers
{
    [Authorize(Roles="admin")]
    public class TranslationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet, ValidateInput(false)]
        public ActionResult UpdatePartial(int id, Table table, Field field)
        {
            Translation translation = db.Translations.FirstOrDefault(x => x.RecordId == id && x.Table == table && x.Field == field);
            if (translation == null)
            {
                translation = new Translation();
                translation.RecordId = id;
                translation.Table = table;
                translation.Field = field;
            }

            return PartialView("_EditPartial", translation);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(Translation model)
        {
            if (ModelState.IsValid)
            {
                Translation translation = db.Translations.FirstOrDefault(x => x.RecordId == model.RecordId && x.Table == model.Table && x.Field == model.Field && x.LanguageId == model.LanguageId);
                db.Entry(translation).State = EntityState.Detached;

                if (translation == null)
                {
                    db.Translations.Add(model);
                }
                else
                {
                    model.Id = translation.Id;
                    db.Entry(model).State = EntityState.Modified;
                }

                db.SaveChanges();

                return Json(new { success = true });
            }

            return Json(new { success = false });
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
