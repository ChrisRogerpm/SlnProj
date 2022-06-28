using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class DocumentoController : Controller
    {
        // GET: Documento
        Documento objDocumento = new Documento();
        Derivacion objDerivacion = new Derivacion();
        public ActionResult Expediente()
        {
            return View();
        }
        public ActionResult Consultar()
        {
            return View();
        }
        public ActionResult ExpedienteDerivar()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ExpedienteListarJson()
        {
            string mensaje = "";
            var data = new List<Documento>();
            try
            {
                data = objDocumento.DocumentoListar();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DocumentoFiltroListarJson(string nroExpediente, string anio)
        {
            string mensaje = "";
            var data = new List<Documento>();
            try
            {
                data = objDocumento.DocumentoFiltroListar(nroExpediente, anio);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DocumentoDerivadoListarJson(string nroExpediente, string anio)
        {
            string mensaje = "";
            var data = new List<Documento>();
            try
            {
                data = objDocumento.DocumentoDerivadoListar(nroExpediente, anio);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ExpedienteDetalleJson(int id)
        {
            string mensaje = "";
            var data = new Documento();
            try
            {
                data = objDocumento.DocumentoDetalle(id);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DocumentoExpedienteDetalleJson(string nroExpediente, string anio)
        {
            string mensaje = "";
            var data = new Documento();
            var listaDerivacion = new List<Derivacion>();
            try
            {
                data = objDocumento.DocumentoExpedienteDetalle(nroExpediente, anio);
                listaDerivacion = objDerivacion.ListaHistorialDerivacion(data.id);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje, listaDerivacion }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ExpedienteRegistrarJson(Documento obj)
        {
            bool respuesta = false;
            string mensaje;
            try
            {
                objDocumento.DocumentoRegistrar(obj);
                respuesta = true;
                mensaje = "Se ha registrado exitosamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { respuesta, mensaje });
        }
        [HttpPost]
        public ActionResult ExpedienteActualizarJson(Documento obj)
        {
            bool respuesta = false;
            string mensaje = "";
            try
            {
                objDocumento.DocumentoActualizar(obj);
                respuesta = true;
                mensaje = "Se ha actualizado exitosamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { respuesta, mensaje });
        }
    }
}