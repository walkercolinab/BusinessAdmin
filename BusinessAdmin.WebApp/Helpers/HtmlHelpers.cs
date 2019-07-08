using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessAdmin.WebApp.Helpers
{
    public static class HtmlHelpers
    {
        public static string MenuIsActive(this HtmlHelper htmlHelper, string[] controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeController = routeData.Values["controller"].ToString();

            var returnActive = controller.Contains(routeController);

            return returnActive ? "active treeview" : "treeview";
        }
        public static string SubMenuIsActive(this HtmlHelper htmlHelper, string controller, string action)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var routeAction = routeData.Values["action"].ToString();
            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController && action == routeAction);

            return returnActive ? "active" : "";
        }

        public static string SubMenuIsActive(this HtmlHelper htmlHelper, string controller)
        {
            var routeData = htmlHelper.ViewContext.RouteData;


            var routeController = routeData.Values["controller"].ToString();

            var returnActive = (controller == routeController);

            return returnActive ? "active" : "";
        }
    }
}