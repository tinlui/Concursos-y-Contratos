//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConcursosContratos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RegistroContratista
    {
        public int IDREGISTRO { get; set; }
        public string REGCONTRALORIAFOLIO { get; set; }
        public string REGCONTRALORIAINGREGSO { get; set; }
        public Nullable<System.DateTime> FECHAEXPEDICION { get; set; }
        public Nullable<System.DateTime> FECHAVIGENCIA { get; set; }
        public Nullable<decimal> CAPITAL { get; set; }
        public Nullable<int> INFCAPITAL { get; set; }
        public Nullable<System.DateTime> FECHAINF { get; set; }
        public Nullable<int> IDCONTRATISTA { get; set; }
        public Nullable<int> IDTIPO { get; set; }
        public Nullable<int> IDESPECIALIDAD { get; set; }
    
        public virtual Contratista Contratista { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        public virtual TipoContratista TipoContratista { get; set; }
    }
}
