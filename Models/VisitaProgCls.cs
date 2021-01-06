using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class VisitaProgCls
    {
        public int? IdDatosLicitacion
        {
            get;set;
        }
        public DateTime? VisitaF
        {
            get; set;
        }
        public string VisitaH
        {
            get; set;
        }
        public int? IdDireccion
        {
            get; set;
        }

        public DateTime? JacFecha
        {
            get; set;
        }
        public string JacHora
        {
            get; set;
        }

        public DateTime? AtecFecha
        {
            get; set;
        }
        public string AtecHora
        {
            get; set;
        }
        public DateTime? ActaFallaFecha
        {
            get; set;
        }
        public string ActaFallaHora
        {
            get; set;
        }
    }
}