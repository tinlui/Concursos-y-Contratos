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
    
    public partial class Contratista
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contratista()
        {
            this.DatosMorals = new HashSet<DatosMoral>();
            this.DatosRepresentas = new HashSet<DatosRepresenta>();
            this.Inscritoes = new HashSet<Inscrito>();
            this.RegistroContratistas = new HashSet<RegistroContratista>();
        }
    
        public int IDCONTRATISTA { get; set; }
        public string RFC { get; set; }
        public string NOMBRE { get; set; }
        public string TELEFONO { get; set; }
        public string CALLE { get; set; }
        public string NOEXTERIOR { get; set; }
        public string NOINTERIOR { get; set; }
        public string COLONIA { get; set; }
        public Nullable<int> CP { get; set; }
        public string CURP { get; set; }
        public Nullable<int> IDMUNICIPIO { get; set; }
        public string CORREO { get; set; }
        public Nullable<int> AÑO { get; set; }
    
        public virtual Municipio Municipio { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatosMoral> DatosMorals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DatosRepresenta> DatosRepresentas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inscrito> Inscritoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RegistroContratista> RegistroContratistas { get; set; }
    }
}
