using BusinessAdmin.WebApp.Models;
using BusinessAdmin.WebApp.Tags;
using BusinessAdmin.WebApp.ViewModel;
using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessAdmin.WebApp.Controllers
{
    [NoLoginAttribute]
    public class LoginController : Controller
    {
        private Usuario um = new Usuario();
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Autenticar(LoginViewModel model)
        //public ActionResult Autenticar(LoginViewModel model)
        {
            var rm = new ResponseModel();

            if (ModelState.IsValid)
            {
                this.um.NombreUsuario = model.NombreUsuario;
                this.um.Password = model.Password;

                rm = um.Autenticarse();

                if (rm.response)
                {
                    rm.href = Url.Content("~/home");
                    //return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                rm.SetResponse(false, "Debe llenar los campos para poder autenticarse.");
            }

            return Json(rm);
        }
    }
}