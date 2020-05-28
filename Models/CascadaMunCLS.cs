using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConcursosContratos.Models
{
    public class CascadingModel
    {
       
    
        public int EntidadId
        {
            get;set;
        }

        public int RegionId
        {
            get; set;
        }

        public int MuncipioId
        {
            get; set;
        }
    }
}