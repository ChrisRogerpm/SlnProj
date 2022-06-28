using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class DerivacionController : Controller
    {
        // GET: Derivacion
        Derivacion objDerivacion = new Derivacion();
        Documento objDocumento = new Documento();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DerivacionRegistrarJson(Derivacion obj)
        {
            bool respuesta = false;
            string mensaje;
            try
            {
                for (int i = 0; i < obj.ListaidTipoDerivacion.Count; i++)
                {
                    obj.idTipoDerivacion = obj.ListaidTipoDerivacion[i];
                    objDerivacion.DerivacionRegistrar(obj);
                }
                respuesta = true;
                mensaje = "Se ha registrado exitosamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { respuesta, mensaje });
        }
        [HttpGet]
        public ActionResult DerivacionDetalleJson(string nroExpediente, string anio)
        {
            string mensaje = "";
            var data = new Derivacion();
            var listaDerivacion = new List<Derivacion>();
            try
            {
                data = objDerivacion.DerivacionDetalle(nroExpediente, anio);
                listaDerivacion = objDerivacion.ListaHistorialDerivacion(data.id);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje,listaDerivacion }, JsonRequestBehavior.AllowGet);
        }


    }
}