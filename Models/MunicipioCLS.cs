using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class MunicipioCLS
    {
        public int IdMunicipio
        {
            get; set;
        }

        public string Municipio
        {
            get; set;
        }

        public int IdRegion
        {
            get; set;
        }

        public string Region
        {
            get;set;
        }

        public int IdEntidad
        {
            get;set;
        }

        public string Entidad
        {
            get;set;
        }
    }
}