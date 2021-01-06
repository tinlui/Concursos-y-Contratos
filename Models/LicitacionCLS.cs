using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class LicitacionCLS
    {
        public int IdDatosLicitacion
        {
            get;set;
        }
        public string NoProceso
        {
            get;set;
        }

        public int? Planos
        {
            get;set;
        }

        public int? Especificaciones
        {
            get;set;
        }

        public string Notas
        {
            get;set;
        }

        public int IdProcedimiento
        {
            get;set;
        }

        public int AÑo
        {
            get;set;
        }

        public int IdTipoObra
        {
            get; set;
        }

        public int IdNivelObra
        {
            get;set;
        }

        public int IdEspObra
        {
            get;set;
        }

        public int IdDireccion
        {
            get;set;
        }
        //tipo contrato
        public int IdSolicitante
        {
            get;set;
        }
    }
}