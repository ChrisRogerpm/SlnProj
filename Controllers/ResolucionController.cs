using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class ResolucionController : Controller
    {
        // GET: Resolucion
        Resolucion objResolucion = new Resolucion();
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Consultar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResolucionRegistrarJson(Resolucion obj)
        {
            bool respuesta = false;
            string mensaje;
            try
            {
                objResolucion.ResolucionRegistrar(obj);
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
        public ActionResult ResolucionListarConsultarJson(string nroExpediente, string anio)
        {
            string mensaje = "";
            var data = new List<Resolucion>();
            try
            {
                data = objResolucion.ResolucionListarConsultar(nroExpediente, anio);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ResolucionDetalleJson(int id)
        {
            string mensaje = "";
            var data = new Resolucion();
            try
            {
                data = objResolucion.ResolucionDetalle(id);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}