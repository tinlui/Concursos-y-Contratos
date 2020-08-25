using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class MontoFinCLS
    {
        public int IdMontoFin
        {
            get;set;
        }
        public int? IdOficioSAut
        {
            get; set;
        }
        public int? IdFuenteFin
        {
            get; set;
        }
        public int? IdEstructuraFin
        {
            get; set;
        }
        public decimal? Monto
        {
            get; set;
        }
       /////Estructura financiera
      public string FuenteFin
        {
            get;set;
        }
       public string EstructuraFin
        {
            get;set;
        }
       ///
    }
}