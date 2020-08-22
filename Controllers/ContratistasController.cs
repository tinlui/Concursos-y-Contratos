using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace ConcursosContratos.Controllers
{
    public class ContratistasController : Controller
    {
        // GET: Contratistas
        public ActionResult Index()
        {
            listarMunicipio();
            return View();
        }
        public JsonResult listarEspecialidad()
        {
            using (var bd = new CCDevEntities())
            {
                var listaEsp = (from item in bd.Especialidads
                                select new SelectListItem
                                {
                                    Text = item.ESPECIALIDAD1,
                                    Value = item.IDESPECIALIDAD.ToString()
                                }).ToList();
                listaEsp.Insert(0, new SelectListItem { Text = "<--ESPECIALIDAD-->" });
                return Json(listaEsp, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listarTipo()
        {
            using(var bd= new CCDevEntities())
            {
                var listaTipo = (from item in bd.TipoContratistas
                                 select new SelectListItem
                                 {
                                     Text = item.TIPO,
                                     Value = item.IDTIPO.ToString()
                                 }).ToList();
                listaTipo.Insert(0, new SelectListItem { Text = "<--TIPO REGISTRO-->" });
                return Json(listaTipo, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult listarCapital()
        {
            using (var bd = new CCDevEntities())
            {
                var listaCap = (from item in bd.InfoCapitals
                                select new SelectListItem
                                { 
                                    Text = item.INFOCAPITAL1, Value = item.IDINFOCAPITAL.ToString() 
                                }).ToList();
                listaCap.Insert(0, new SelectListItem { Text = "<--INFO. CAPITAL-->" });
                return Json(listaCap, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listarIdentificacion()
        {
            using(var bd= new CCDevEntities())
            {
                var listaIden = (from item in bd.TblIdentificacions
                                 select new SelectListItem
                                 {
                                     Text=item.DESCRIPCION,Value=item.IDIDENTIFICACION.ToString()
                                 }).ToList();
                listaIden.Insert(0, new SelectListItem { Text = "<--IDENTIFICACION-->" });
                return Json(listaIden, JsonRequestBehavior.AllowGet);
            }
        }

        public void listarMunicipio()
        {
            List<SelectListItem> listaMun;
            using(var bd = new CCDevEntities())
            {
                listaMun = (from item in bd.Municipios
                            select new SelectListItem
                            {
                                Text = item.MUNICIPIO1,
                                Value = item.IDMUNICIPIO.ToString()
                            }).ToList();
                listaMun.Insert(0, new SelectListItem { Text = "<--Municipio-->" });
                ViewBag.listarMun = listaMun;
            }
        }

        public string GuardarNombreContratista(ContratistaCLS contratistaCLS)
        {
            string rpta = "";
            try
            { 
                using(var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        int cant = 0;
                        cant = bd.Contratistas.Where(c => c.NOMBRE.Contains(contratistaCLS.Nombre)).Count();
                        if (cant.Equals(1))
                        {
                            rpta = "Ya existe";
                        }
                        else
                        {
                                Contratista contratista = new Contratista();
                                contratista.NOMBRE = contratistaCLS.Nombre;

                                bd.Contratistas.Add(contratista);
                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                        }
                    }
                }
            }catch(Exception ex)
            {
                rpta = "error" + ex;
            }
            return rpta;
        }
        public string GuardarContratista(ContratistaCLS contratistaCLS)
        {
            string rpta = "";
            try
            {
                using(var bd = new CCDevEntities())
                {
                    using (var tran= new TransactionScope())
                    {
                        if (!ModelState.IsValid)
                        {
                            var query = (from state in ModelState.Values
                                         from error in state.Errors
                                         select error.ErrorMessage).ToList();
                            rpta += "<ul class='list-group'>";
                            foreach (var item in query)
                            {
                                rpta += "<li class='list-group-item'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else { 
                        int cant = 0;
                        cant = bd.Contratistas.Where(c => c.NOMBRE.Contains(contratistaCLS.Nombre)).Count();
                        if (cant.Equals(1))
                        {
                            Contratista contratista = bd.Contratistas.Where(c => c.NOMBRE == contratistaCLS.Nombre).First();
                            contratista.RFC = contratistaCLS.Rfc;
                            contratista.TELEFONO = contratistaCLS.Telefono;
                            contratista.CALLE = contratistaCLS.Calle;
                            contratista.NOEXTERIOR = contratistaCLS.NoExterior;
                            contratista.NOINTERIOR = contratistaCLS.NoInterior;
                            contratista.COLONIA = contratistaCLS.Colonia;
                            contratista.CP = contratistaCLS.Cp;
                            contratista.CURP = contratistaCLS.Curp;
                            contratista.IDMUNICIPIO = contratistaCLS.IdMunicipio;
                            contratista.CORREO = contratistaCLS.Correo;
                            contratista.AÑO = contratistaCLS.Año;
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                        else
                        {
                                Contratista contratista = new Contratista();
                                contratista.RFC = contratistaCLS.Rfc;
                                contratista.NOMBRE = contratistaCLS.Nombre;
                                contratista.TELEFONO = contratistaCLS.Telefono;
                                contratista.CALLE = contratistaCLS.Calle;
                                contratista.NOEXTERIOR = contratistaCLS.NoExterior;
                                contratista.NOINTERIOR = contratistaCLS.NoInterior;
                                contratista.COLONIA = contratistaCLS.Colonia;
                                contratista.CP = contratistaCLS.Cp;
                                contratista.CURP = contratistaCLS.Curp;
                                contratista.IDMUNICIPIO = contratistaCLS.IdMunicipio;
                                contratista.CORREO = contratistaCLS.Correo;
                                contratista.AÑO = contratistaCLS.Año;

                                bd.Contratistas.Add(contratista);
                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }
                    }
                    }
                }
            }catch(Exception ex)
            {
                rpta = "error" + ex;
            }
            return rpta;
        }
        public string GuardarMoral(MoralCLS moralCLS)
        {
            string rpta = "";
            try
            { 
                using(var bd= new CCDevEntities())
                {
                    using(var tran= new TransactionScope())
                    {
                        if (!ModelState.IsValid)
                        {
                            var query = (from state in ModelState.Values
                                         from error in state.Errors
                                         select error.ErrorMessage).ToList();
                            rpta += "<ul class='list-group'>";
                            foreach (var item in query)
                            {
                                rpta += "<li class='list-group-item list-group-item-danger'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else { 
                        Contratista contratista = bd.Contratistas.Where(c => c.NOMBRE == moralCLS.NombreCont).First();
                        moralCLS.IdContratista = contratista.IDCONTRATISTA;
                            DatosMoral datosMoral = new DatosMoral();
                            datosMoral.ACTACONSTITUTIVA = moralCLS.ActaConstitutiva;
                            datosMoral.FECHAACTA = moralCLS.FechaActa;
                            datosMoral.NOTARIONUM = moralCLS.NotarioNum;
                            datosMoral.NOTARIONOMBRE = moralCLS.NotarioNombre;
                            datosMoral.REGPUBLICO = moralCLS.RegPublico;
                            datosMoral.IDMUNICIPIO = moralCLS.IdMunicipio;
                            datosMoral.IDCONTRATISTA = moralCLS.IdContratista;

                            bd.DatosMorals.Add(datosMoral);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }catch(Exception ex) 
            {
                rpta = "error" + ex;
            }
            return rpta;
        }
        public string GuardarRegistro(RegistroContratistaCLS registroContratistaCLS)
        {
            string rpta = "";
            try
            {
                using(var bd = new CCDevEntities())
                {
                    using (var tran=new TransactionScope())
                    {
                        if (!ModelState.IsValid) {
                            var query = (from state in ModelState.Values
                                         from error in state.Errors
                                         select error.ErrorMessage).ToList();
                            rpta += "<ul class='list-group'>";
                            foreach (var item in query)
                            {
                                rpta += "<li class='list-group-item list-group-item-danger'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else{
                            Contratista contratista = bd.Contratistas.Where(c => c.NOMBRE == registroContratistaCLS.NombreCont).First();
                            registroContratistaCLS.IdContratista = contratista.IDCONTRATISTA;

                            int cant = 0;
                            cant = bd.RegistroContratistas.Where(c => c.IDCONTRATISTA.Equals(registroContratistaCLS.IdContratista)).Count();
                            if (cant.Equals(1))
                            {
                                RegistroContratista registroContratista = bd.RegistroContratistas.Where(c => c.IDCONTRATISTA == registroContratistaCLS.IdContratista).First();
                                registroContratista.REGCONTRALORIAFOLIO = registroContratistaCLS.RegContraloriaFolio;
                                registroContratista.REGCONTRALORIAINGREGSO = registroContratistaCLS.RegContraloriaIngreso;
                                registroContratista.FECHAEXPEDICION = registroContratistaCLS.FechaExpedicion;
                                registroContratista.FECHAVIGENCIA = registroContratistaCLS.FechaVigencia;
                                registroContratista.CAPITAL = registroContratistaCLS.Capital;
                                registroContratista.IDINFOCAPITAL = registroContratistaCLS.IdInfoCapital;
                                registroContratista.FECHAINF = registroContratistaCLS.FechaInf;
                                registroContratista.IDTIPO = registroContratistaCLS.IdTipo;
                                registroContratista.IDESPECIALIDAD = registroContratistaCLS.IdEspecialidad;
                                registroContratista.ACTIVO = registroContratistaCLS.Activo;

                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            } else { 

                            RegistroContratista registroContratista = new RegistroContratista();
                            registroContratista.REGCONTRALORIAFOLIO = registroContratistaCLS.RegContraloriaFolio;
                            registroContratista.REGCONTRALORIAINGREGSO = registroContratistaCLS.RegContraloriaIngreso;
                            registroContratista.FECHAEXPEDICION = registroContratistaCLS.FechaExpedicion;
                            registroContratista.FECHAVIGENCIA = registroContratistaCLS.FechaVigencia;
                            registroContratista.CAPITAL = registroContratistaCLS.Capital;
                            registroContratista.IDINFOCAPITAL = registroContratistaCLS.IdInfoCapital;
                            registroContratista.FECHAINF = registroContratistaCLS.FechaInf;
                            registroContratista.IDCONTRATISTA = registroContratistaCLS.IdContratista;
                            registroContratista.IDTIPO = registroContratistaCLS.IdTipo;
                            registroContratista.IDESPECIALIDAD = registroContratistaCLS.IdEspecialidad;
                            registroContratista.ACTIVO = registroContratistaCLS.Activo;

                            bd.RegistroContratistas.Add(registroContratista);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                rpta = "error: " + ex;
            }
            return rpta;
        }
        public string GuardarRepresentante(RepresentaCLS RepresentaCLS)
        {
            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        if (!ModelState.IsValid)
                        {
                            var query = (from state in ModelState.Values
                                         from error in state.Errors
                                         select error.ErrorMessage).ToList();
                            rpta += "<ul class='list-group'>";
                            foreach (var item in query)
                            {
                                rpta += "<li class='list-group-item list-group-item-danger'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else
                        {
                            Contratista contratista = bd.Contratistas.Where(c => c.NOMBRE == RepresentaCLS.nombreCont).First();
                            RepresentaCLS.IdContratista = contratista.IDCONTRATISTA;

                            int cant = 0;
                            cant = bd.DatosRepresentas.Where(c => c.IDCONTRATISTA.Equals(RepresentaCLS.IdContratista)).Count();
                            if (cant.Equals(1))
                            {
                                DatosRepresenta datosRepresenta = bd.DatosRepresentas.Where(c => c.IDCONTRATISTA == RepresentaCLS.IdContratista).First();
                                datosRepresenta.REPRESENTADA = RepresentaCLS.Representada;
                                datosRepresenta.PUESTO = RepresentaCLS.Puesto;
                                datosRepresenta.ACREDITA = RepresentaCLS.Acredita;
                                datosRepresenta.NACREDITA = RepresentaCLS.NAcredita;
                                datosRepresenta.IDIDENTIFICACION = RepresentaCLS.IdIdentificacion;
                                datosRepresenta.NIDEN = RepresentaCLS.NIden;

                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }
                            else { 
                            
                            DatosRepresenta datosRepresenta = new DatosRepresenta();
                            datosRepresenta.REPRESENTADA = RepresentaCLS.Representada;
                            datosRepresenta.PUESTO = RepresentaCLS.Puesto;
                            datosRepresenta.ACREDITA = RepresentaCLS.Acredita;
                            datosRepresenta.NACREDITA = RepresentaCLS.NAcredita;
                            datosRepresenta.IDIDENTIFICACION = RepresentaCLS.IdIdentificacion;
                            datosRepresenta.NIDEN = RepresentaCLS.NIden;
                            datosRepresenta.IDCONTRATISTA = RepresentaCLS.IdContratista;


                            bd.DatosRepresentas.Add(datosRepresenta);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                            }
                        }
                    }
                }
            }catch(Exception ex)
            {
                rpta = "error: " + ex; 
            }
            return rpta;
        }
        public string GuardarPoder(PoderCLS poderCLS)
        {
            string rpta="";
            try
            {
                using (var bd= new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        if (!ModelState.IsValid)
                        {
                            var query = (from state in ModelState.Values
                                         from error in state.Errors
                                         select error.ErrorMessage).ToList();
                            rpta += "<ul class='list-group'>";
                            foreach (var item in query)
                            {
                                rpta += "<li class='list-group-item list-group-item-danger'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else
                        {

                            DatosRepresenta datosRepresenta  = bd.DatosRepresentas.Where(dr => dr.REPRESENTADA == poderCLS.NombrePod).First();
                            poderCLS.IdRepresenta = datosRepresenta.IDREPRESENTA;

                            int cant = 0;
                            cant = bd.DatosPoders.Where(c => c.IDREPRESENTA.Equals(poderCLS.IdRepresenta)).Count();

                            if (cant.Equals(1))
                            {
                                DatosPoder datosPoder = bd.DatosPoders.Where(c => c.IDREPRESENTA == poderCLS.IdRepresenta).First();
                                datosPoder.PODEREP = poderCLS.PoderRep;
                                datosPoder.FECHA = poderCLS.Fecha;
                                datosPoder.NOTARIONO = poderCLS.NotarioNo;
                                datosPoder.NOTARIONOMBRE = poderCLS.NotarioNombre;
                                datosPoder.IDMUNICIPIO = poderCLS.IdMunPoder;

                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }
                            else
                            {
                                DatosPoder datosPoder = new DatosPoder();
                                datosPoder.IDREPRESENTA = poderCLS.IdRepresenta;
                                datosPoder.PODEREP = poderCLS.PoderRep;
                                datosPoder.FECHA = poderCLS.Fecha;
                                datosPoder.NOTARIONO = poderCLS.NotarioNo;
                                datosPoder.NOTARIONOMBRE = poderCLS.NotarioNombre;
                                datosPoder.IDMUNICIPIO = poderCLS.IdMunPoder;


                                bd.DatosPoders.Add(datosPoder);
                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                rpta = "error: " + ex;
            }
            return rpta;
        }
    }
}