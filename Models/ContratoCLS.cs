using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class ContratoCLS
    {
        public int IdContrato
        {
            get;set;
        }
        public int? IdOficiosAut
        {
            get;set;
        }
        public string NoContrato
        {
            get;set;
        }
        public int? IdTipoContrato
        {
            get;set;
        }
        public string DescripcionObra
        {
            get;set;
        }
        public int? OficioAprobacion
        {
            get;set;
        }
        public DateTime? FechaAprob
        {
            get;set;
        }
        public DateTime? FechaRecAprob
        {
            get;set;
        }
        public DateTime? InicioObra
        {
            get;set;
        }
        public DateTime? FinObra
        {
            get;set;
        }
        public int? DiasContrato
        {
            get;set;
        }
        public DateTime? FechaContrato
        {
            get;set;
        }
        public int? IdMunicipio
        {
            get;set;
        }
        public int? Habilitado
        {
            get;set;
        }
        public string NotasAclaratorias
        {
            get;set;
        }
        public int? IdDatosLicitacion
        {
            get;set;
        }
    }
}