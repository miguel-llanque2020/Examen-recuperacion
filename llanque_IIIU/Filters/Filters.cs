using llanque_IIIU.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace SGCS.Filters {
   
    public class AutenticadoAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession()) {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {
                    controller = "Login",
                    action = "Index"
                }));
            }
        }
    }
    
    public class NoLoginAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession()) {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new {
                    controller = "Proyecto",
                    action = "Index"
                }));
            }
        }
    }
}