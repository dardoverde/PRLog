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
    
    public partial class PerfilesEntrenadores
    {
        public decimal id { get; set; }
        public Nullable<decimal> idUsuario { get; set; }
        public Nullable<decimal> idDisciplina { get; set; }
        public string Observacion { get; set; }
    
        public virtual Disciplinas Disciplinas { get; set; }
        public virtual Usuarios Usuarios { get; set; }
    }
}
