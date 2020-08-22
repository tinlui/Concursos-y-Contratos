using System;
using System.Collections.Generic;
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

        public DateTime FechaExpedicion
        {
            get;set;
        }

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
    }
}