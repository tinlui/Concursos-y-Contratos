using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class OficiosAutCLS
    {
        public string OficioAut
        {
            get;set;
        }

        public DateTime? FecAutorizacion
        {
            get;set;
        }

        public DateTime? FecRecibido
        {
            get; set;
        }

        public int? NumAsignacion
        {
            get;set;
        }

        public int? NumObra
        {
            get;set;
        }

        public string DescObra
        {
            get;set;
        }
        public int? IdAutoriza
        {
            get;set;
        }
        public decimal? MontoAutorizado
        {
            get;set;
        }

        public int? IdPrograma
        {
            get;set;        
        }

        public int? IdMunicipio
        {
            get;set;        
        }
        /// auxiliares
        /// 
        public string Autorizado
        {
            get;set;        
        }
        public string Progamado
        {
            get;set;
        }
        /// <summary>
        /// dropdownlist
        /// </summary>
        public string Muni
        {
            get;set;
        }
        public int EntidadId
        {
            get; set;
        }
        public string Entidad
        {
            get; set;
        }
        public int RegionId
        {
            get; set;
        }
        public string Region
        {
            get; set;
        }
    }
}