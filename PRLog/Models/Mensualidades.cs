//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PRLog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Mensualidades
    {
        public decimal id { get; set; }
        public Nullable<System.DateTime> FechaDesde { get; set; }
        public Nullable<System.DateTime> FechaHasta { get; set; }
        public Nullable<decimal> Dias { get; set; }
        public Nullable<decimal> idUsuario { get; set; }
        public Nullable<decimal> idMembresia { get; set; }
        public Nullable<decimal> idDisciplina { get; set; }
        public string Estado { get; set; }
        public Nullable<decimal> Valor { get; set; }
    
        public virtual Disciplinas Disciplinas { get; set; }
        public virtual Membresias Membresias { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
