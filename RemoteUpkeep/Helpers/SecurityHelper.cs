using System.Web;

namespace RemoteUpkeep.Helpers
{
    public static class SecurityHelper
    {
        public static bool AdminVisible()
        {
            return
               HttpContext.Current.User != null &&
               HttpContext.Current.User.Identity.IsAuthenticated &&
               (
                HttpContext.Current.User.Identity.Name == "mark.vlasenko@gmail.com" ||
                HttpContext.Current.User.IsInRole("admin")
               );
        }

        public static bool DealerVisible()
        {
            return HttpContext.Current.User != null &&
               HttpContext.Current.User.Identity.IsAuthenticated && 
               HttpContext.Current.User.IsInRole("dealer");
        }

    }
}