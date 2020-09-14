using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcursosContratos.Models
{
    public class ContratistaCLS
       
    {
        public int IdContratista
        {
            get;set;
        }
        [Required]
        public string Nombre
        {
            get; set;
        }
        [StringLength(10, ErrorMessage = "Maximo es de {1}")]
        public string Rfc
        {
            get;set;
        }
        public string Curp
        {
            get; set;
        }
            [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Correo
        {
            get;set;
        }
        public string Telefono
        {
            get;set;
        }

        public string Fax
        {
            get;set;
        }
        public string Calle
        {
            get; set;
        }
        public string Colonia
        {
            get; set;
        }
    

        public string NoExterior
        {
            get;set;
        }

        public string NoInterior
        {
            get;set;        
        }


        public int Cp
        {
            get;set;
        }

        public int IdMunicipio
        {
            get;set;
        }
        public int Año
        {
            get; set;
        }
    

    
        //
        public string municipio
        {
            get;set;
        }
        public string region
        {
            get; set;
        }
        public string entidad
        {
            get; set;
        }
    }
}