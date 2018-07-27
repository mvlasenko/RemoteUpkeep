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
               HttpContext.Current.User.Identity.Name == "mark.vlasenko@gmail.com"; //todo: user type based security
        }
    }
}