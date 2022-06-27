using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        Usuario objUsuario = new Usuario();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Listar()
        {
            return View();
        }
        [HttpGet]
        public ActionResult UsuarioDetalleJson(int id)
        {
            string mensaje = "";
            var data = new Usuario();
            try
            {
                data = objUsuario.UsuarioDetalle(id);
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UsuarioListarJson()
        {
            string mensaje = "";
            var data = new List<Usuario>();
            try
            {
                data = objUsuario.UsuarioListar();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { data, mensaje }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UsuarioRegistrarJson(Usuario obj)
        {
            bool respuesta = false;
            string mensaje;
            try
            {
                objUsuario.UsuarioRegistrar(obj);
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
        public ActionResult UsuarioEditarJson(Usuario obj)
        {
            bool respuesta = false;
            string mensaje;
            try
            {
                objUsuario.UsuarioEditar(obj);
                respuesta = true;
                mensaje = "Se ha editado exitosamente";
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { respuesta, mensaje });
        }
        [HttpPost]
        public ActionResult ValidarLoginJson(Usuario obj)
        {
            bool respuesta = false;
            string mensaje = "";
            var data = new Usuario();
            try
            {
                data = objUsuario.ValidarLogin(obj);
                if (data.id != 0)
                {
                    if (data.estado == 0)
                    {
                        mensaje = "El usuario ha sido deshabilitado";
                    }
                    else
                    {
                        Session["id"] = data.id;
                        Session["nombreCompleto"] = data.nombreCompleto;
                        Session["UsuarioFull"] = data;
                        respuesta = true;
                        mensaje = "Bienvenido(a) al sistema";
                    }
                }
                else
                {
                    mensaje = "Las credenciales no son correctas";
                }

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return Json(new { respuesta, mensaje, data });
        }
        [HttpPost]
        public ActionResult CerrarSesionLoginJson()
        {
            string mensaje = "";
            bool respuesta = false;
            try
            {
                Session["id"] = null;
                Session["nombreCompleto"] = null;
                Session["UsuarioFull"] = null;
                respuesta = true;
            }
            catch (Exception exp)
            {
                mensaje = exp.Message + " ,Llame Administrador";
            }
            return Json(new { respuesta, mensaje });
        }
        [HttpPost]
        public ActionResult UsuarioCambiarEstadoJson(Usuario obj)
        {
            string mensaje = "";
            bool respuesta = false;
            try
            {
                objUsuario.UsuarioCambiarEstado(obj);
                respuesta = true;
                mensaje = "Se ha actualizado el estado exitosamente";
            }
            catch (Exception exp)
            {
                mensaje = exp.Message + " ,Llame Administrador";
            }
            return Json(new { respuesta, mensaje });
        }
    }
}