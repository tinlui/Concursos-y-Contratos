using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class RegistroContratistaCLS
    {
        public string RegContraloriaFolio
        {
            get;set;
        }

        public string RegContraloriaIngreso
        {
            get;set;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaExpedicion
        {
            get;set;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaVigencia
        {
            get;set;
        }

        public decimal Capital
        {
            get;set;
        }

        public int IdInfoCapital
        {
            get;set;
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInf
        {
            get;set;
        }

        public int IdContratista
        {
            get;set;
        }

        public int IdTipo
        {
            get;set;
        }

        public int IdEspecialidad
        {
            get;set;
        }

        public bool Activo
        {
            get;set;
        }

        public string NombreCont
        {
            get;set;
        }

        public string InfoCapital
        {
            get;set;
        }

        public string Tipo
        {
            get;set;
        }
        public string Especialidad
        {
            get;set;
        }
    }
}