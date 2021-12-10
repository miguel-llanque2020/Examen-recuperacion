using llanque_IIIU.Helpers;
using llanque_IIIU.Models;
using SGCS.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace llanque_IIIU.Controllers
{
    
    public class ProyectoController : Controller
    {
        USUARIO usuario = LoginController.usuarioLogeado;
        // GET: Proyecto


        public ActionResult Index()
        {            
            if (LoginController.usuarioLogeado == null) {
                return Redirect("~/Login");
            }
            

            List<PROYECTO> proyectos = new List<PROYECTO>();
            switch (usuario.NIVEL) {
                case "ADMIN":
                    proyectos = PROYECTO.getProjects();
                    break;
                case "DOCENTE":
                    proyectos = PROYECTO.getProjects().Where(x => x.USUARIO.ID_PERSONA == usuario.ID_PERSONA).ToList();
                    break;
                case "ESTUDIANTE":
                    proyectos = PROYECTO.getProjects();
                    break;
            }

            ViewBag.proyectos = proyectos;            

            return View();
        }

        public ActionResult DetalleProyecto(int cod =0) {
            if (usuario == null) {
                return Redirect("~/Login");
            }
            ViewBag.Proyecto = PROYECTO.getProjectById(cod);            
            ViewBag.Estudiantes = getEstudiantes(LoginController.usuarioLogeado.PERSONA.ID_PERSONA);
            ViewBag.Miembros = PERSONA_PROYECTO.getPersona_ProyectoByProject(cod);
            return View(new PERSONA_PROYECTO());
        }

        public ActionResult AgregarMiembroAProyecto(PERSONA_PROYECTO obj) {            
            if (!PERSONA_PROYECTO.createProject(obj)) {
                return Redirect("~/Proyecto/DetalleProyecto?cod="+obj.ID_PROYECTO);
            }

            return Redirect("~/Proyecto/DetalleProyecto?cod=" + obj.ID_PROYECTO);
        }

        public ActionResult SalirProyecto(int cod = 0) {
            if(cod == 0) {
                return Redirect("~/Proyecto");
            }
            PROYECTO proyecto = PERSONA_PROYECTO.getAllMembers().Where(x => x.ID_PERSONA_PROYECTO == cod).Select(x => x.PROYECTO).First();
            PERSONA_PROYECTO.deleteMember(cod);
            var miembros = PERSONA_PROYECTO.getPersona_ProyectoByProject(proyecto.ID_PROYECTO);
            if (miembros.Count == 0) {
                PROYECTO.changeStateAvailable(proyecto.ID_PROYECTO);
            }
            return Redirect("~/Proyecto/misProyectos");
        }

        public ActionResult AgregarMiembro(int cod =0 ,int user = 0) {
            if (cod == 0 || user == 0 || LoginController.usuarioLogeado == null) {
                return Redirect("~/Proyecto");
            }
            PERSONA_PROYECTO obj = new PERSONA_PROYECTO();
            obj.ID_PROYECTO = cod;
            obj.ID_PERSONA = user;
            if (!PERSONA_PROYECTO.createProject(obj)) {
                return Redirect("~/Proyecto");
            }

            PROYECTO.changeState(cod);                       

            return Redirect("~/Proyecto/DetalleProyecto?cod=" + cod);
        }

        public ActionResult misProyectos() {
            if (usuario == null) {
                return Redirect("~/Login");
            }
            var proyectos = PERSONA_PROYECTO.getAllMembers().Where(x => x.ID_PERSONA == usuario.ID_PERSONA).Select(x => x.PROYECTO).ToList();
            return View(proyectos);
        }

        public ActionResult cerrarSesion() {
            LoginController.usuarioLogeado = null;
            SessionHelper.DestroyUserSession();
            return Redirect("~/Login");
        }

        public ActionResult AgregarEditar(int cod = 0) {
            if (usuario == null) {
                return Redirect("~/Login");
            }         
           
            ViewBag.Docentes = getDocentes();
            ViewBag.Lineas = getLineas(); 
            return View(cod == 0 ? new PROYECTO()//nuevoObjeto
                                : PROYECTO.getProjectById(cod)
                       );
        }

        private List<SelectListItem> getEstudiantes(int user) {
            var seleccionar = new SelectListItem() {
                Text = "Seleccione estudiante",
                Value = "",
                Selected = true,
                Disabled = false
            };

            List<SelectListItem> docentes = USUARIO.getUsers().Where(x => x.NIVEL.Equals("ESTUDIANTE") & x.ID_PERSONA!=user).ToList().ConvertAll(d => {
                return new SelectListItem() {
                    Text =  "Dni: "+ d.PERSONA.DNI+" , "+ d.PERSONA.APELLIDOS + ", " + d.PERSONA.NOMBRES,
                    Value = d.ID_PERSONA.ToString(),
                    Selected = false
                };
            });

            docentes.Insert(0, seleccionar);
            return docentes;
        }

        private List<SelectListItem> getDocentes() {
            var seleccionar = new SelectListItem() {
                Text = "Seleccione docente",
                Value = "",
                Selected = true,
                Disabled = false
            };

            List<SelectListItem> docentes = USUARIO.getUsers().Where(x => x.NIVEL.Equals("DOCENTE")).ToList().ConvertAll(d => {
                return new SelectListItem() {
                    Text = d.PERSONA.APELLIDOS + ", " + d.PERSONA.NOMBRES,
                    Value = d.ID_USUARIO.ToString(),
                    Selected = false
                };
            });

            docentes.Insert(0, seleccionar);
            return docentes;
        }

        private List<SelectListItem> getLineas() {
            var seleccionar = new SelectListItem() {
                Text = "Seleccione linea de investigación",
                Value = "",
                Selected = true,
                Disabled = false
            };

            List<SelectListItem> lineas = LINEA_INVESTIGACION.getLineas().ToList().ConvertAll(d => {
                return new SelectListItem() {
                    Text = d.NOMBRE,
                    Value = d.ID_LINEA.ToString(),
                    Selected = false
                };
            });

            lineas.Insert(0, seleccionar);
            return lineas;
        }

        public ActionResult Guardar(PROYECTO model, int cod = 0, int tipo = 99) {
            if (ModelState.IsValid) {
                model.ESTADO = "1";
                if (!PROYECTO.createProject(model)) {
                    ViewBag.Message = "error";
                    return Redirect("~/Proyecto");
                };
                ViewBag.Message = "success";
                return Redirect("~/Proyecto");
            } else {
                ViewBag.Valido = false;
                return Redirect("~/Proyecto/AgregarEditar");
            }
        }

        public ActionResult Eliminar(int cod) {            
            PROYECTO.Eliminar(cod);
            return Redirect("~/Proyecto");
        }
    }
}