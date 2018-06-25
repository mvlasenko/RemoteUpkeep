using System.Collections.Generic;
using System.Linq;
using RemoteUpkeep.Models;

namespace RemoteUpkeep.Helpers
{
    public static class UIHelper
    {
        public static List<Country> GetCountries()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Countries.ToList();
            }
        }

        public static List<Language> GetLanguages()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Languages.ToList();
            }
        }
    }
}