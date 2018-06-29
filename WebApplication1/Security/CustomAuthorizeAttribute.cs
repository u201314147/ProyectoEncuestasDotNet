using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Inei.ViewModel;
namespace WebApplication1.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (SessionPersister.usuario == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "LoginAdmin" }));
            }
            else
            {
                LoginViewModel cuentavm = new LoginViewModel();
                CustomPrincipal mp = new CustomPrincipal(cuentavm.findUser(SessionPersister.usuario.Codigo));

                if (!mp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "AccesoDenegado" }));
                }
            }
        }
    }
}