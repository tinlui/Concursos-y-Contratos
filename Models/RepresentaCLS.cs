using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class RepresentaCLS
    {

        public string Representada
        {
            get;set;
        }
        
        public string Puesto
        {
            get;set;
        }

        public int Acredita
        {
            get;set;
        }
        //CAMBIAR A STRING CUANDO SE VUELVA A CREAR LA BD
        public int NAcredita
        {
            get;set;
        }

        public int IdIdentificacion
        {
            get;set;
        }
        public string NIden
        {
            get; set;
        }
        public int IdContratista
        {
            get;set;
        }
        public string nombreCont
        {
            get;set;
        }
    }
}