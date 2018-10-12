using System.Collections.Generic;
using System.Linq;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Helpers
{
    public static class TranslationHelper
    {
        public static List<Service> GetServices()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Service> res = context.Services.ToList();

                var x = 
                (
                    from s in context.Services
                    join t in context.Translations
                    on s.Id equals t.RecordId
                    where t.Table == Table.Services && t.Field == Field.Title && t.LanguageId == 5 
                    select new ServiceViewModel { Id = s.Id, Description = s.Description, Title = t.TranslationValue, CreatedDateTime = s.CreatedDateTime, CreatedByUserId = s.CreatedByUserId }
                 ).ToList();

                return res;
            }
        }

    }
}