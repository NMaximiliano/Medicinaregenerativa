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
    
    public partial class Calendario
    {
        public int idCalendario { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public string Hora { get; set; }
        public int idTurno { get; set; }
        public Nullable<System.DateTime> FechaCarga { get; set; }
        public string UserId { get; set; }
    
        public virtual Turnos Turnos { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
