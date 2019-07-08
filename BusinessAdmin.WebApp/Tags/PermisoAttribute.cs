﻿using BusinessAdmin.WebApp.Models;
using BusinessAdmin.WebApp.Models.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BusinessAdmin.WebApp.Tags
{
    public class PermisoAttribute : ActionFilterAttribute
    {
        public ePermiso Permiso { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!FrontUser.TienePermiso(this.Permiso))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Home",
                    action = "Index"
                }));
            }
        }
    }
}