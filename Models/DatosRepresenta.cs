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
    
    public partial class DatosRepresenta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DatosRepresenta()
        {
            this.DatosPoders = new HashSet<DatosPoder>();
        }
    
        public int IDREPRESENTA { get; set; }
        public string REPRESENTADA { get; set; }
        public string PUESTO { get; set; }
        public Nullable<int> IDIDENTIFICACION { get; set; }
        public string NIDEN { get; set; }
        public Nullable<int> ACREDITA { get; set; }
        public string NACREDITA { get; set; }
        public Nullable<int> IDCONTRATISTA { get; set; }
    
        public virtual Contratista Contratista { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatosPoder> DatosPoders { get; set; }
        public virtual TblIdentificacion TblIdentificacion { get; set; }
    }
}
