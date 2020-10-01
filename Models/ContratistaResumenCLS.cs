using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcursosContratos.Models
{
    public class ContratistaResumenCLS
    {
        public int IdContratista
        {
            get;set;
        }
        /*-------------------------------------*/
        /*----------Generales------------------*/
        /*-------------------------------------*/
        public string Nombre
        {
            get;set;
        }

        public string Rfc
        {
            get;set;
        }

        public string Curp
        {
            get;set;
        }

        public int AñoReg
        {
            get;set;
        }

        public string Tel
        {
            get;set;
        }

        public string Correo
        {
            get;set;
        }

        /*-------------------------------------*/
        /*----------Personales-----------------*/
        /*-------------------------------------*/

        public string Colonia
        {
            get;set;
        }

        public int Cp
        {
            get;set;
        }

        public string Calle
        {
            get;set;
        }

        public string NoExt
        {
            get;set;
        }

        public string NoInt
        {
            get;set;
        }

        public string Municipio
        {
            get;set;
        }

        public string Region
        {
            get;set;
        }

        public string Entidad
        {
            get;set;
        }

        /*-------------------------------------*/
        /*----------Moral----------------------*/
        /*-------------------------------------*/

        public int? ActaConstitutiva
        {
            get;set;
        }

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
        public string MunicipioMor
        {
            get; set;
        }

        /*-------------------------------------*/
        /*----------Registro-------------------*/
        /*-------------------------------------*/

        public string Folio
        {
            get;set;
        }

        public string Ingreso
        {
            get;set;
        }

        public DateTime? Expedicion
        {
            get;set;
        }

        public DateTime? Vigencia
        {
            get;set;
        }

        public decimal? Capital
        {
            get;set;
        }

        public string InfoCapital
        {
            get;set;
        }

        public DateTime? FechaInf
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

        public bool? Activo
        {
            get;set;
        }


        /*-------------------------------------*/
        /*----------Representante--------------*/
        /*-------------------------------------*/

        public string Representada
        {
            get;set;
        }

        public string Puesto
        {
            get;set;
        }

        public string Acredita
        {
            get;set;
        }

        public string NAcredita
        {
            get;set;
        }

        public string Identificacion
        {
            get;set;
        }

        public string NIden
        {
            get;set;
        }


        /*-------------------------------------*/
        /*----------Poder----------------------*/
        /*-------------------------------------*/

        public int? PoderRep
        {
            get;set;
        }

        public DateTime? Fecha
        {
            get;set;
        }

        public int? NotNo
        {
            get;set;
        }

        public string NotNombre
        {
            get;set;
        }

        public string MunPoder
        {
            get;set;
        }

        public string NombrePod
        {
            get;set;
        }
    }
}