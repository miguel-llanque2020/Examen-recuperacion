using llanque_IIIU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace llanque_IIIU.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (LoginController.usuarioLogeado == null) {
                return Redirect("~/Login");
            }
            return View(USUARIO.getUsers());
        }
    }
}