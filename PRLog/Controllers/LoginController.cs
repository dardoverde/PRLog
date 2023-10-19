using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using PRLog.Extensions;

namespace PRLog.Controllers
{
    public class LoginController : Controller
    {

        [HttpPost]
        public ActionResult Login(string txtNombre, string txtContrasena, string chkRecuerdame)
        {
            try
            {

                var miUsuario = UsuariosExtension.LoginUsuario(txtNombre, txtContrasena);

                SetCookiesYVariablesDeUsuario(miUsuario, chkRecuerdame, txtContrasena);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                return View();
            }
        }


        // GET: Login
        public ActionResult Login(int? G)
        {
            if (G is null)
            {
                ViewBag.Registro = false;
            }
            else { ViewBag.Registro = true; ViewBag.idGimnasio = G; }

            try
            {
                return AutenticaDesdeCookie();
            }
            catch (Exception ex)
            {
                Logout();
                ViewBag.Error = ex.Message;
            }


                return View();
        }

        private ActionResult AutenticaDesdeCookie() {
            if (Request.Cookies[ConfigurationManager.AppSettings["RecuerdameCookie"]] != null)
            {
                string result = ConfigurationManager.AppSettings["RecuerdameCookie"];
                string txtNombre = Request.Cookies[result].Values["EmailID"];
                string txtContrasena = Request.Cookies[result].Values["Password"];

                var miUsuario = UsuariosExtension.LoginUsuario(txtNombre, txtContrasena);

                SetCookiesYVariablesDeUsuario(miUsuario, "off", "");

                return RedirectToAction("Index", "Home");

            }
            else return View();
        }



        private void setUserCookies(Models.Usuarios miUsuario)
        {
            ViewBag.miUsuario = miUsuario;
            Session["User"] = miUsuario;
            Session["NombreUsuario"] = miUsuario.Nombre + " " + miUsuario.Apellido;
            //List<RolesXUsuarios> roles = UsuariosExtension.getRoles(miUsuario.id);
            //Session["Roles"] = roles;

            //RolesXUsuarios RolProgramador = roles.Where(x => x.idRol == 2 || x.idRol == 3).FirstOrDefault();
            //Session["esProgramador"] = (RolProgramador != null);
            //TempData["esProgramador"] = (RolProgramador != null);

            //RolesXUsuarios RolAdministrador = roles.Where(x => x.idRol == 2 || x.idRol == 3).FirstOrDefault();
            //Session["esAdministrador"] = (RolAdministrador != null);
        }

        private void Recuerdame(string txtNombre, string txtContrasena)
        {


            HttpCookie cookie = new HttpCookie(ConfigurationManager.AppSettings["RecuerdameCookie"]);
            cookie.Values.Add("EmailID", txtNombre);
            cookie.Values.Add("Password", txtContrasena);

            cookie.Expires = DateTime.Now.AddDays(15);
            Response.Cookies.Add(cookie);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            try
            {
                Session["User"] = null;

                HttpCookie cookie = new HttpCookie(ConfigurationManager.AppSettings["RecuerdameCookie"])
                {
                    Expires = DateTime.Now.AddDays(-1) // or any other time in the past
                };
                Response.Cookies.Set(cookie);

                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            catch (Exception)
            {

                throw new Exception("Error. Logout.");
            }

        }

        // GET: Login
        public ActionResult Registry(int? G)
        {
            
            if (G is null)
            {
                ViewBag.Registro = false;
            }
            else { ViewBag.Registro = true; ViewBag.idGimnasio = G; }

            return AutenticaDesdeCookie();


            //return View();
        }

        [HttpPost]
        public ActionResult Registry(string txtNombre, string txtApellido, string txtCelular, DateTime dteFechaNacimiento, string txtEmail, string txtContrasena, string txtContrasenaConfirma)
        {
            try
            {
                if (txtContrasena != txtContrasenaConfirma)
                {
                    throw new Exception(Recursos.Recurso.ErrorConfirmaContrasena);
                }
                else { 
                var miUsuario = UsuariosExtension.CreaUsuario(txtNombre, txtApellido, txtCelular, dteFechaNacimiento, txtEmail, txtContrasena);

                SetCookiesYVariablesDeUsuario(miUsuario, "off", "");
               

                return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        private void SetCookiesYVariablesDeUsuario(Models.Usuarios miUsuario, string chkRecuerdame, string contrasena) {
            if (miUsuario != null)
            {

                setUserCookies(miUsuario);

                if (chkRecuerdame == "on")
                {
                    Recuerdame(miUsuario.Email, contrasena);
                }

            }
            else 
            {
                throw new Exception("Error usuario no existente");

            }

            }
    }
}