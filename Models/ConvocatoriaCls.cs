using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class ConvocatoriaCls
    {
        public int? IdDatosLicitacion
        {
            get;set;
        }

        public DateTime? Proceso
        {
            get;set;
        }

        public DateTime? ProcesoAut
        {
            get; set;
        }
        public DateTime? ConvocatoriaDo
        {
            get; set;
        }
        public DateTime? EnvioCn
        {
            get; set;
        }
        public DateTime? Publicacion
        {
            get; set;
        }
        public DateTime? FLimite1
        {
            get; set;
        }
        public DateTime? FLimite2
        {
            get; set;
        }
        public DateTime? RecepcionLicitar
        {
            get; set;
        }
    }
}