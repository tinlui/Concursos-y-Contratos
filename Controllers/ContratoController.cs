using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace ConcursosContratos.Controllers
{
    public class ContratoController : Controller
    {
        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        #region combos
        public JsonResult listaContratistas()
        {
            bool res = true;
            using (var bd = new CCDevEntities())
            {
                var listaContEmp = (from item in bd.RegistroContratistas
                                    join t in bd.Contratistas on
                                    item.IDCONTRATISTA equals t.IDCONTRATISTA
                                    join idt in bd.TipoContratistas on
                                    item.IDTIPO equals idt.IDTIPO
                                    where item.ACTIVO == res
                                    select new SelectListItem
                                    {
                                        Value = item.IDREGISTRO.ToString(),
                                        Text = t.NOMBRE +" | "+ idt.TIPO

                                    }).ToList();
                return Json( listaContEmp,JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult listaDesicion()
        {
            using(var bd = new CCDevEntities())
            {
                var listaDesicion = (from item in bd.Desicions
                                     select new SelectListItem
                                     {
                                         Value = item.IDDESICION.ToString(),
                                         Text = item.DESICION1
                                     }).ToList();
                return Json(listaDesicion, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult listaFalla()
        {
            using (var bd = new CCDevEntities())
            {
                var listaFalla = (from item in bd.DesicionFallas
                                  select new SelectListItem
                                  {
                                      Value = item.IDDESICIONFALLA.ToString(),
                                      Text = item.DESICIONFALLA1
                                  }).ToList();

                return Json(listaFalla, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaLicitacionOficio(string numProceso)
        {
        using (var bd = new CCDevEntities())
            {
                var listaLicitacionOficio = (from item in bd.LicitacionOficioAuts
                                             join item2 in bd.OficiosAuts on
                                             item.IDOFICIOSAUT equals item2.IDOFICIOSAUT
                                             join item3 in bd.DatosLicitacions on
                                             item.IDDATOSLICITACION equals item3.IDDATOSLICITACION
                                             where item3.NOPROCESO == numProceso
                                             select new SelectListItem
                                             {
                                                 Value = item.IDLICOFAUT.ToString(),
                                                 Text = item2.OFICIOAUT
                                             }).ToList();
                return Json(listaLicitacionOficio, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region insert
        public string guardaContrato(ContratoCLS contratoCLS)
        {
            int noContrato = 0;
            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        noContrato = bd.Contratoes.Where(c => c.NOCONTRATO == contratoCLS.NoContrato).Count();
                        if (noContrato.Equals(1))
                        {
                            rpta = "Ya existe";
                        }
                        else
                        {
                            Contrato contrato = new Contrato();
                            contrato.NOCONTRATO = contratoCLS.NoContrato;
                            contrato.FECHACONTRATO = contratoCLS.FechaContrato;
                            bd.Contratoes.Add(contrato);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }
            catch (Exception ex){
                rpta = "error " + ex.Message;
            }
            return rpta;
        }
        #endregion
    }
}