using System.Web;
using System.Web.Mvc;

namespace PRLog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filtro de autenticaci[on de usuario
            filters.Add(new Filters.VerifySession());
        }
    }
}
