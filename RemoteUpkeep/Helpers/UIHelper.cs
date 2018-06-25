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

        public static List<Region> GetRegions()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Regions.ToList();
            }
        }

        public static List<Language> GetLanguages()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Languages.ToList();
            }
        }

        public static List<Service> GetServices()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Services.ToList();
            }
        }

    }
}