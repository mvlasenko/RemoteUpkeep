using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace RemoteUpkeep.Helpers
{
    public static class TranslationHelper
    {
        public static Language GetCurrentLanguage()
        {
            //return primary language if user is authenticated

            if (HttpContext.Current.User.Identity.IsAuthenticated) {

                string userName = HttpContext.Current.User.Identity.Name;
                using (var context = new ApplicationDbContext())
                {
                    ApplicationUser user = context.Users.Include(x => x.PrimaryLanguage).FirstOrDefault(x => x.UserName == userName);
                    if (user != null && user.PrimaryLanguage != null)
                        return user.PrimaryLanguage;
                }
            }

            //return language stored in cookies 

            string name = Thread.CurrentThread.CurrentCulture.Name;
            Language language = null;

            using (var context = new ApplicationDbContext())
            {
                language = context.Languages.FirstOrDefault(x => x.Code == name);
                if (language == null)
                    language = context.Languages.FirstOrDefault();
            }

            return language;
        }

        public static void SetCulture(int id)
        {
            Language language = null;

            using (var context = new ApplicationDbContext())
            {
                language = context.Languages.FirstOrDefault(x => x.Id == id);
                if (language == null)
                    language = context.Languages.FirstOrDefault();
            }

            string culture = language.Code;

            // Save culture in a cookie
            HttpCookie cookie = HttpContext.Current.Request.Cookies["_culture"];
            if (cookie != null)
            {
                cookie.Value = culture;   // update cookie value
            }
            else
            {
                cookie = new HttpCookie("_culture");
                cookie.Value = culture;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static List<ServiceViewModel> GetServices()
        {
            int languageId = GetCurrentLanguage().Id;

            using (var context = new ApplicationDbContext())
            {
                return
                (
                    from s in context.Services
                    join t1 in context.Translations.Where(t => t.Table == Table.Services && t.Field == Field.Title && t.LanguageId == languageId) on s.Id equals t1.RecordId into st1
                    from t1 in st1.DefaultIfEmpty()
                    join t2 in context.Translations.Where(t => t.Table == Table.Services && t.Field == Field.Description && t.LanguageId == languageId) on s.Id equals t2.RecordId into st2
                    from t2 in st2.DefaultIfEmpty()
                    select new ServiceViewModel { Id = s.Id, Title = t1.TranslationValue ?? s.Title, Description = t2.TranslationValue ?? s.Description, CreatedDateTime = s.CreatedDateTime, CreatedByUserId = s.CreatedByUserId }
                 ).ToList();
            }
        }

    }
}