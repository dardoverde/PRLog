using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRLog.Models;

namespace PRLog.Extensions
{
    public class MembresiasExtension
    {
        public static List<Membresias> getMembresias()
        {
            using (var db = new AppDBEntities())
            {

                List<Membresias> misMembresias = db.Membresias.ToList();

                

                return misMembresias;

            }
        }
    }
}