using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRLog.Extensions;
using System.Net.Mail;
using System.Net.Mime;

namespace PRLog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var miUsuario = (PRLog.Models.Usuarios)Session["User"];

            ViewBag.Usuario = miUsuario;

            ViewBag.misMensualidades = UsuariosExtension.GetMensualidades(miUsuario.id);


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AdminGim(int? G)
        {

            var miUsuario = (Models.Usuarios)Session["User"];

            ViewBag.Gimnasios = UsuariosExtension.getGimnasios(miUsuario);
            ViewBag.Disciplinas = UsuariosExtension.getDisciplinas(miUsuario);
            if (!(G is null) && ViewBag.Gimnasios.Count() > 0)
            {
                ViewBag.idGimnasio = ViewBag.Gimnasios[0].id;
            } else {
                ViewBag.idGimnasio = G;
            }

            return View();
        }

        public ActionResult AdminUsuarios() {

            var miUsuario = (Models.Usuarios)Session["User"];
            List<Models.Gimnasios> misGimnasios = UsuariosExtension.getGimnasios(miUsuario);
            foreach (var miGimnasio in misGimnasios)
            {
                miGimnasio.Disciplinas = GimnasiosExtension.GetDisciplinas(miGimnasio.id);
            }
            ViewBag.Gimnasios = misGimnasios;

            ViewBag.Usuarios = UsuariosExtension.getUsuariosTotal();

            return View();
        }
        public ActionResult AdminUsu() {
            int U = Convert.ToInt32(Url.RequestContext.RouteData.Values["id"]);

            var miUsuario = (Models.Usuarios)Session["User"];

            List<Models.Gimnasios> misGimnasios = UsuariosExtension.getGimnasios(miUsuario);
            foreach (var miGimnasio in misGimnasios)
            {
                miGimnasio.Disciplinas = GimnasiosExtension.GetDisciplinas(miGimnasio.id);
            }
            ViewBag.Gimnasios = misGimnasios;

            Models.Usuarios Atleta = UsuariosExtension.getUsuario(U);
            Atleta.Mensualidades = UsuariosExtension.GetMensualidades(Atleta);
            ViewBag.Atleta = Atleta;

            ViewBag.Membresias = MembresiasExtension.getMembresias();

            return View();

        }

        [HttpPost]
        public ActionResult AddMensualidades(DateTime dteFechaDesde, DateTime dteFechaHasta, decimal txtDias, decimal txtIdAtleta, decimal selMembresia, decimal selDisciplina, decimal txtValor) {
            try
            {

                UsuariosExtension.AddMensualidad(dteFechaDesde, dteFechaHasta, txtDias, txtIdAtleta, selMembresia, selDisciplina, "Act", txtValor);

                return Redirect("AdminUsu/" + txtIdAtleta);
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
                Console.WriteLine(ex.Message);
                //return Redirect("AdminUsu/" + txtIdAtleta);
                return View();
            }
        }


        [HttpPost]
        public void BtnEnviar_Click(object sender, EventArgs e)
        {
            EnviarCorreo();
        }

        private void EnviarCorreo()
        {

            MailMessage email = new MailMessage();
            SmtpClient smtp = new SmtpClient();

            email.To.Add(new MailAddress("imprimelo9@gmail.com"));
            email.From = new MailAddress("imprimelo9@gmail.com");
            email.Subject = "Notificación ( " + DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss") + " ) ";
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = "Tu mensaje | tu firma";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
           // FileStream fs = new FileStream("E:\\TestFolder\\test.pdf", FileMode.Open, FileAccess.Read);
            //Attachment a = new Attachment(fs, "test.pdf", MediaTypeNames.Application.Octet);
            //email.Attachments.Add(a);

            smtp.Host = "smtp.gmail.com";  // IP empresa/institucional
                                          //smtp.Host = "smtp.hotmail.com";
                                          //smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.Timeout = 50;
            smtp.EnableSsl = false;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("imprimelo9@gmail.com", "unodostres");

            string lista = "antoniolarco@gmail.com";
            string output = "";

            var mails = lista.ToString().Split(';');
            foreach (string dir in mails)
                email.To.Add(dir);

            try
            {
                email.Body = "Prueba n[umero 1";
                smtp.Send(email);
                email.Dispose();
                output = "Correo electrónico fue enviado satisfactoriamente.";
            }
            catch (SmtpException exm)
            {
                throw new Exception (exm.Message);
            }
            catch (Exception ex)
            {
                output = "Error enviando correo electrónico: " + ex.Message;
            }

            //MessageBox.Show(output);
        }
    
    }
}