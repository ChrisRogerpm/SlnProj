using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class TipoDerivacionController : Controller
    {
        // GET: TipoDerivacion
        TipoDerivacion objTipoDerivacion = new TipoDerivacion();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TipoDerivacionListarJson()
        {
            string mensaje = "";
            var data = new List<TipoDerivacion>();
            try
            {
                data = objTipoDerivacion.TipoDerivacionListar();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}