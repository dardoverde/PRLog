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
    
    public partial class Disciplinas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disciplinas()
        {
            this.Horarios = new HashSet<Horarios>();
            this.PerfilesEntrenadores = new HashSet<PerfilesEntrenadores>();
            this.Mensualidades = new HashSet<Mensualidades>();
        }
    
        public decimal id { get; set; }
        public string Nombre { get; set; }
        public string Observaciones { get; set; }
        public Nullable<decimal> idGimnasio { get; set; }
    
        public virtual Gimnasios Gimnasios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Horarios> Horarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PerfilesEntrenadores> PerfilesEntrenadores { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mensualidades> Mensualidades { get; set; }
    }
}
