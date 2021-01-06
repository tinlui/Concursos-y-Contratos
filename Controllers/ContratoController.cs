using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        #endregion
    }
}