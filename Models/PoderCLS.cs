using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class PoderCLS
    {
        public int IdRepresenta
        {
            get;set;
        }
    public int PoderRep
        {
            get;set;
        }
        public DateTime Fecha
        {
            get;set;
        }
        public int NotarioNo
        {
            get;set;
        }
        public string NotarioNombre
        {
            get;set;
        }
        public int IdMunPoder
        {
            get;set;
        }
        public string NombrePod
        {
            get;set;
        }
    }
}