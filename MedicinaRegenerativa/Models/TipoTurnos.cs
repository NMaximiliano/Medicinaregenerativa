//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicinaRegenerativa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TipoTurnos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoTurnos()
        {
            this.Turnos = new HashSet<Turnos>();
            this.Presupuestos = new HashSet<Presupuestos>();
        }
    
        public int idTipoTurno { get; set; }
        public string Descripcion { get; set; }
        public Nullable<System.DateTime> FechaCarga { get; set; }
        public string UserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Turnos> Turnos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Presupuestos> Presupuestos { get; set; }
    }
}
