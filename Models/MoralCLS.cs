using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class MoralCLS
    {
        [Display(Name = "Acta Constitutiva")]
        public int? ActaConstitutiva
        {
            get;set;
        }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? FechaActa
        {
            get;set;
        }
        public int? NotarioNum
        {
            get;set;
        }
        public string NotarioNombre
        {
            get;set;
        }
        public int? RegPublico
        {
            get;set;
        }
        public int IdMunicipio
        {
            get;set;
        }
        public int IdContratista
        {
            get;set;
        }
        public string NombreCont
        {
            get;set;
        }
        public string Municipio
        {
            get;set;
        }
    }
}