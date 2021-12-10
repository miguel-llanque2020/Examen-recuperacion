using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace llanque_IIIU.Models {
    public static class HtmlUtility {

        public static string IsActive(this HtmlHelper html,
                                 string control,
                                 string action) {
            var routeData = html.ViewContext.RouteData;

            var routeControl = (string)routeData.Values["controller"];

            // must match both
            var returnActive = control == routeControl;


            return returnActive ? "nav-link active" : "nav-link";
        }
    }
}