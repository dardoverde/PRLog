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
    
    public partial class Horarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horarios()
        {
            this.Registros = new HashSet<Registros>();
        }
    
        public decimal id { get; set; }
        public string Nombre { get; set; }
        public string HoraInicio { get; set; }
        public string HoraFin { get; set; }
        public Nullable<decimal> Capacidad { get; set; }
        public Nullable<decimal> Dia { get; set; }
        public Nullable<decimal> idDisciplina { get; set; }
    
        public virtual Disciplinas Disciplinas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Registros> Registros { get; set; }
    }
}
