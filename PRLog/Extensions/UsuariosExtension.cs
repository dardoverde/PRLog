using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRLog.Models;

namespace PRLog.Extensions
{
    public class UsuariosExtension
    {
        public static Usuarios LoginUsuario(string nombre, string contrasena)
        {
            Usuarios usuario = null;

            //quitar esto para producci[on
            //if (nombre == "" && contrasena == "")
            //{
            //    nombre = "antoniolarco@gmail.com";
            //    contrasena = "ABCD";
            //}

            //contrasena = Tools.GetSHA256(contrasena.Trim());
            

            using (var db = new AppDBEntities())
            {
                Autenticaciones Autenticacion = (from u in db.Autenticaciones
                              where u.Contrasenia == contrasena && u.Usuario == nombre
                              select u).FirstOrDefault();

                if (Autenticacion == null) throw new Exception(Recursos.Recurso.ErrorUsuarioNoEncontrado);
                else { 
                 usuario = (from d in db.Usuarios
                               where d.Email == nombre.Trim()
                               select d).FirstOrDefault();

                if (usuario == null) throw new Exception("Error: Usuario no encontrado o contraseña incorrecta.");
                }
                   
                return usuario;
            }

        }

        public static Usuarios getUsuario(int idUsuario)
        {

            using (var db = new AppDBEntities())
            {

                db.Configuration.LazyLoadingEnabled = false;
                var usuario = (from d in db.Usuarios
                               where d.id == idUsuario
                               select d).FirstOrDefault();

                return usuario;
            }
        }

        public static List<Gimnasios> getGimnasios(Usuarios Usuario) {
            using (var db = new AppDBEntities()) {


                db.Configuration.LazyLoadingEnabled = false;

                List<Administradores> Administracion = db.Administradores.Include("Gimnasios").Where(x => x.idUsuario == Usuario.id).ToList();

                List<Gimnasios> misGimnasios = new List<Gimnasios>();
                foreach (var Administra in Administracion)
                {
                    //misGimnasios.Add((from g in db.Gimnasios
                    //                 where g.id == Administra.idGimnasio
                    //                 select g).FirstOrDefault());
                    misGimnasios.Add(Administra.Gimnasios);
                }

                return misGimnasios;
            
            }
        }

        public static List<Mensualidades> GetMensualidades(Usuarios Usuario) {

            return GetMensualidades(Usuario.id);
        }
        public static List<Mensualidades> GetMensualidades(decimal idUsuario)
        {

            using (var db = new AppDBEntities())
            {

                db.Configuration.LazyLoadingEnabled = false;

                List<Mensualidades> misMensualidades = db.Mensualidades.Include("Disciplinas").Include("Membresias").Where(x => x.idUsuario == idUsuario && x.FechaHasta >= DateTime.Now).OrderByDescending(m => m.id).ToList();
                foreach (var mensualidad in misMensualidades)
                {
                    mensualidad.Disciplinas.Gimnasios = db.Gimnasios.Where(x => x.id == mensualidad.Disciplinas.idGimnasio).ToList()[0];
                }
                return misMensualidades;

            }
        }


        public static List<Usuarios> getUsuariosTotal() {
            using (var db = new AppDBEntities())
            {
                var usuario = (from d in db.Usuarios

                               select d).ToList();

                return usuario;
            }
        }
        public static List<Disciplinas> getDisciplinas(Usuarios Usuario)
        {
            using (var db = new AppDBEntities())
            {

                List<PerfilesEntrenadores> PerfilesEntrenador = db.PerfilesEntrenadores.Include("Disciplinas").Where(x => x.idUsuario == Usuario.id).ToList();

                List<Disciplinas> misDisciplinas = new List<Disciplinas>();
                foreach (var Entrenador in PerfilesEntrenador)
                {
                    misDisciplinas.Add(Entrenador.Disciplinas);
                }

                return misDisciplinas;

            }
        }


        public static Usuarios CreaUsuario(string Nombre, string Apellido, string Celular, DateTime FechaNacimiento, string Email, string Contrasena) {
            Usuarios miUsuario = null;

            using (var db = new AppDBEntities())
            {

                //revisar si el usuario ya existe
                miUsuario = (from u in db.Usuarios
                                      where u.Email == Email
                                      select u).FirstOrDefault();
                if (miUsuario != null) throw new Exception(Recursos.Recurso.ErrorUsuarioExistente);
                else {
                    miUsuario = new Usuarios();
                    miUsuario.Nombre = Nombre;
                    miUsuario.Apellido = Apellido;
                    miUsuario.Celular = Celular;
                    miUsuario.FechaNacimiento = FechaNacimiento;
                    miUsuario.Email = Email;
                    db.Usuarios.Add(miUsuario);
                    db.SaveChanges();

                    Autenticaciones miAutenticacion = new Autenticaciones();
                    miAutenticacion.Usuario = Email;
                    miAutenticacion.Contrasenia = Contrasena;
                    miAutenticacion.idUsuario = miUsuario.id;
                    db.Autenticaciones.Add(miAutenticacion);
                    db.SaveChanges();
                }

                return miUsuario;
            }
        }

        //Funciones operativas de mensualidades

        public static bool AddMensualidad(DateTime FechaDesde, DateTime FechaHasta, decimal Dias, decimal idUsuario, decimal idMembresia, decimal idDisciplina, string Estado, decimal Valor) {
            using (var db = new AppDBEntities())
            {
                Mensualidades miMensualidad = new Mensualidades();

                //filtros
                if (FechaHasta <= FechaDesde)
                {
                    throw new Exception(Recursos.Recurso.ErrorFechaInicioFin);
                }
                if (Dias < 1)
                {
                    throw new Exception(Recursos.Recurso.ErrorDiasMayores);
                }
                if (Valor < 0)
                {
                    throw new Exception(Recursos.Recurso.ErrorValorNegativo);
                }

                miMensualidad.FechaDesde = FechaDesde;
                miMensualidad.FechaHasta = FechaHasta;
                miMensualidad.Dias = Dias;
                miMensualidad.idUsuario = idUsuario;
                miMensualidad.idMembresia = idMembresia;
                miMensualidad.idDisciplina = idDisciplina;
                miMensualidad.Estado = Estado;
                if ( Valor <= 0)
                {
                    throw new Exception("Valor debe ser mayor a 0");
                }
                miMensualidad.Valor = Valor;

                db.Mensualidades.Add(miMensualidad);
                db.SaveChanges();

            }
            return true;
        }

    }
}