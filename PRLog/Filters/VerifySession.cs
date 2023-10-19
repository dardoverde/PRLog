using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PRLog.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        private PRLog.Models.Usuarios usuario;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {

                var actionMethodName = filterContext.ActionDescriptor.ActionName;

                if (actionMethodName != "About" && actionMethodName != "Contact" && actionMethodName != "Info")
                {
                    base.OnActionExecuting(filterContext);
                    usuario = (PRLog.Models.Usuarios)HttpContext.Current.Session["User"];
                    if (usuario == null)
                    {
                        if (filterContext.Controller is Controllers.LoginController == false)
                        {
                            filterContext.HttpContext.Response.Redirect("~/Login/Login",true);
                            throw new Exception("User not found");
                        }
                    }

                }


            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
        }
    }
}