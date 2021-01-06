using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class AnticipoCls
    {
        public int? IdDatosLicitacion
        {
            get;set;
        }

        public decimal? Anticipo
        {
            get;set;
        }

        public decimal AnticipoInicio
        {
            get;set;
        }

        public decimal? AnticipoMateriales
        {
            get;set;
        }

        public decimal? Iva
        {
            get;set;
        }

        public decimal? CapitalMinimo
        {
            get;set;
        }
    }
}