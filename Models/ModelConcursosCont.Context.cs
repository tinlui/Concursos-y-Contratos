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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CCDevEntities : DbContext
    {
        public CCDevEntities()
            : base("name=CCDevEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Afianzadora> Afianzadoras { get; set; }
        public virtual DbSet<Anticipo> Anticipoes { get; set; }
        public virtual DbSet<Autoriza> Autorizas { get; set; }
        public virtual DbSet<Cancelado> Canceladoes { get; set; }
        public virtual DbSet<Contratista> Contratistas { get; set; }
        public virtual DbSet<Contrato> Contratoes { get; set; }
        public virtual DbSet<Convocatoria> Convocatorias { get; set; }
        public virtual DbSet<DatosLicitacion> DatosLicitacions { get; set; }
        public virtual DbSet<DatosMoral> DatosMorals { get; set; }
        public virtual DbSet<DatosPoder> DatosPoders { get; set; }
        public virtual DbSet<DatosRepresenta> DatosRepresentas { get; set; }
        public virtual DbSet<Desicion> Desicions { get; set; }
        public virtual DbSet<DesicionFalla> DesicionFallas { get; set; }
        public virtual DbSet<Direccion> Direccions { get; set; }
        public virtual DbSet<Entidad> Entidads { get; set; }
        public virtual DbSet<Especialidad> Especialidads { get; set; }
        public virtual DbSet<EspObra> EspObras { get; set; }
        public virtual DbSet<EstructuraFin> EstructuraFins { get; set; }
        public virtual DbSet<Fianza> Fianzas { get; set; }
        public virtual DbSet<FuenteFin> FuenteFins { get; set; }
        public virtual DbSet<InfoCapital> InfoCapitals { get; set; }
        public virtual DbSet<Inscrito> Inscritoes { get; set; }
        public virtual DbSet<LicitacionOficioAut> LicitacionOficioAuts { get; set; }
        public virtual DbSet<MontoFin> MontoFins { get; set; }
        public virtual DbSet<MotivoCancelacion> MotivoCancelacions { get; set; }
        public virtual DbSet<Municipio> Municipios { get; set; }
        public virtual DbSet<NivelObra> NivelObras { get; set; }
        public virtual DbSet<OficiosAut> OficiosAuts { get; set; }
        public virtual DbSet<Procedimiento> Procedimientoes { get; set; }
        public virtual DbSet<Programa> Programas { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RegistroContratista> RegistroContratistas { get; set; }
        public virtual DbSet<Responsabilidad> Responsabilidads { get; set; }
        public virtual DbSet<Seguimiento> Seguimientoes { get; set; }
        public virtual DbSet<TblIdentificacion> TblIdentificacions { get; set; }
        public virtual DbSet<TipoContratista> TipoContratistas { get; set; }
        public virtual DbSet<TipoContrato> TipoContratoes { get; set; }
        public virtual DbSet<TipoObra> TipoObras { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<VisitaProg> VisitaProgs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
