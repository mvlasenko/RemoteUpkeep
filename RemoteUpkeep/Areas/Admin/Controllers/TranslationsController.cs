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
            Translation translation = GetDefaultTranslation(id, table, field);
            return PartialView("_EditPartial", translation);
        }

        private Translation GetDefaultTranslation(int id, Table table, Field field)
        {
            Translation translation = new Translation();
            translation.RecordId = id;
            translation.Table = table;
            translation.Field = field;
            translation.LanguageId = 0;

            if (table == Table.Services)
            {
                Service service = db.Services.Find(id);
                if (field == Field.Title)
                {
                    translation.OriginalValue = service.Title;
                }
                else if (field == Field.Description)
                {
                    translation.OriginalValue = service.Description;
                }
            }

            translation.TranslationValue = translation.OriginalValue;

            return translation;
        }

        [HttpGet, ValidateInput(false)]
        public ActionResult GetTranslation(int id, int? languageId, Table table, Field field)
        {
            Translation translation = languageId == null ?
                GetDefaultTranslation(id, table, field) :
                db.Translations.FirstOrDefault(x => x.RecordId == id && x.Table == table && x.Field == field && x.LanguageId == languageId);

            return Json(translation, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult UpdatePartial(Translation model)
        {
            if (ModelState.IsValid)
            {
                Translation translation = db.Translations.FirstOrDefault(x => x.RecordId == model.RecordId && x.Table == model.Table && x.Field == model.Field && x.LanguageId == model.LanguageId);

                if (translation == null)
                {
                    db.Translations.Add(model);
                }
                else
                {
                    model.Id = translation.Id;
                    db.Entry(translation).State = EntityState.Detached;
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
