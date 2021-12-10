using llanque_IIIU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace llanque_IIIU.Controllers
{
    public class LoginController : Controller
    {
        public static USUARIO usuarioLogeado = null;

        // GET: Login
        public ActionResult Index()
        {
            if (usuarioLogeado != null) {
                return Redirect("~/Proyecto");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login objLogin) {
            if (USUARIO.login(objLogin.usuario, objLogin.clave)) {
                usuarioLogeado = USUARIO.getUserByNick(objLogin.usuario);
                return Redirect("~/Proyecto");
            }
            ViewBag.error = "error";
            return View();
        }
    }
}