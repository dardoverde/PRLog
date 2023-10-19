using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRLog.Models;

namespace PRLog.Extensions
{
    public class GimnasiosExtension
    {
        public static List<Disciplinas> GetDisciplinas(decimal idGimnasio) {

            using (var db = new AppDBEntities())
            {
                //db.Configuration.LazyLoadingEnabled = false;
                return db.Disciplinas.Where(x => x.idGimnasio == idGimnasio).ToList();
            }
            
        }
    }
}