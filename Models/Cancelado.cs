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
    
    public partial class Cancelado
    {
        public int IDCANCELADO { get; set; }
        public Nullable<int> IDDATOSLICITACION { get; set; }
        public Nullable<int> IDMOTIVOCANCELACION { get; set; }
        public string MOTIVO { get; set; }
    
        public virtual DatosLicitacion DatosLicitacion { get; set; }
        public virtual MotivoCancelacion MotivoCancelacion { get; set; }
    }
}
