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
    
    public partial class Inscrito
    {
        public int IDINSCRITO { get; set; }
        public Nullable<int> IDCONTRATO { get; set; }
        public Nullable<int> IDCONTRATISTA { get; set; }
        public Nullable<int> ATECNICA { get; set; }
        public Nullable<int> AECONOMICA { get; set; }
        public Nullable<decimal> AECONOMICAMONTO { get; set; }
        public Nullable<int> AFALLO { get; set; }
        public string TECMOTIVO { get; set; }
        public string ECOMOTIVO { get; set; }
        public Nullable<int> TIEMPOEJE { get; set; }
        public Nullable<decimal> FALLO { get; set; }
        public Nullable<int> IDDESICIONFALLA { get; set; }
        public Nullable<int> IDDESICION { get; set; }
        public Nullable<int> GANADOR { get; set; }
    
        public virtual Contratista Contratista { get; set; }
        public virtual Contrato Contrato { get; set; }
        public virtual Desicion Desicion { get; set; }
        public virtual DesicionFalla DesicionFalla { get; set; }
    }
}
