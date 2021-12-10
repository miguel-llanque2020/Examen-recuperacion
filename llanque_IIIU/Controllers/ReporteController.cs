using llanque_IIIU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace llanque_IIIU.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            if (LoginController.usuarioLogeado==null || !LoginController.usuarioLogeado.NIVEL.Equals("ADMIN")) {
                return Redirect("~/Proyecto");
            }
            ViewBag.TotalProyectos = PROYECTO.getProjects().Count;
            ViewBag.chartObject = createChartObject();
            return View(LINEA_INVESTIGACION.getLineas());
        }

        private string createChartObject() {
            string chartObject = "";
            var lineas = LINEA_INVESTIGACION.getLineas();
            foreach(var linea in lineas) {
                chartObject +=  $"{{" +
                                $"  type:'column'," +
                                $"  name:'{linea.NOMBRE}'," +
                                $"  data:[{linea.PROYECTO.Count}]," +
                                $"}},";
            }                     
            double totalProyectos = PROYECTO.getProjects().Count;
            double totalLineas = lineas.Count;
            double media = Math.Round(totalProyectos / totalLineas,2);
            chartObject +=      $"{{" +
                                $"  type:'spline'," +
                                $"  name:'promedio'," +
                                $"  data:[{media}]," +
                                $"  marker:{{" +
                                $"      lineWidth: 2," +
                                $"      lineColor: Highcharts.getOptions().colors[3]," +
                                $"      fillColor: 'white'" +
                                $"  }}" +
                                $"}},{{" +
                                $"  type:'pie'," +
                                $"  name:'Cantidad'," +
                                $"  data:[";

            int i = 0;
            foreach (var linea in lineas) {
                chartObject +=  $"   {{" +                                
                                $"      name:'{linea.NOMBRE}'," +
                                $"      y:{linea.PROYECTO.Count}," +
                                $"      color: Highcharts.getOptions().colors[{i}]" +
                                $"   }},";
                i++;
            }
            chartObject +=      $"      ]," +
                                $"  center:[100,80]," +
                                $"  size:100," +
                                $"  showInLegend:false," +
                                $"  dataLabels:{{" +
                                $"    enabled:false  " +
                                $"  }}" +
                                $"}}";

            return chartObject;
        }


   
}
}