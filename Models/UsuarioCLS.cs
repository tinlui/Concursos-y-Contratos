using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ConcursosContratos.Models
{
    public class UsuarioCLS
    {

        public int idusuario
        {
            get;set;
        }
        [Required]
        [StringLength(8,ErrorMessage ="{0} Maximo es de {1}",MinimumLength =7)]
        [Display(Name = "Usuario")]
        public string usuario
        {
            get; set;
        }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(8, ErrorMessage = "{0} Maximo es de {1}", MinimumLength = 7)]
        [Display(Name ="Password")]
        public string contra
        {
            get; set;
        }

       [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessage = "Maximo es de {1}")]
        public string descripcion
        {
            get; set;
        }

        [Required]
        [StringLength(100, ErrorMessage = "Maximo es de {1}")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string correo
        {
            get; set;
        }

        public int? idresponsabilidad
        {
            get; set;
        }

        public int activo
        {
            get; set;
        }

        //Adicional
        public string responsabilidad
        {
            get; set;
        }
    }
}