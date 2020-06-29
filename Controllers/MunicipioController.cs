using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Transactions;
using ConcursosContratos.Models;
using System.Web.UI.WebControls;
using System.Data.Entity.Infrastructure;

namespace ConcursosContratos.Controllers
{
    public class MunicipioController : Controller
    {

        public JsonResult listaRegion(int Entidadid)
        {
            using (var bd = new CCDevEntities())
            {
                var listItems = bd.Municipios
                              .Join(bd.Regions,
                             mun => mun.IDREGION,
                             ent => ent.IDREGION,
                             (mun, ent) => new { ent.IDREGION, ent.REGION1, mun })
                              .Where(x => x.mun.IDENTIDAD == Entidadid)
                              .GroupBy(x => x.IDREGION)
                              .Select(x => x.FirstOrDefault()).ToList();

                return Json(listItems, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaEntidad()
        {
          
            using (var bd = new CCDevEntities())
            {
                var regionList =(from item in bd.Entidads
                                 select new SelectListItem { Text=item.ENTIDAD1,Value=item.IDENTIDAD.ToString() }).ToList();
                return Json(regionList, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Municipio
        public ViewResult Index(int? page)
        {
        

            using (var bd = new CCDevEntities())
            {
                var listaMunicipios = (from m in bd.Municipios
                                       join e in bd.Entidads
                                       on m.IDENTIDAD
                                       equals e.IDENTIDAD
                                       join r in bd.Regions
                                       on m.IDREGION
                                       equals r.IDREGION
                                       orderby m.MUNICIPIO1
                                       select new MunicipioCLS
                                       {
                                           Municipio = m.MUNICIPIO1,
                                           Entidad = e.ENTIDAD1,
                                           Region = r.REGION1
                                       }).ToList();
                int pageSize = 10;
                int pageNumber = (page ?? 1);
                return View(listaMunicipios.ToPagedList(pageNumber,pageSize));
            }
            
        }

        public ActionResult Filtrar(string municipiobuscar, int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            using(var bd= new CCDevEntities())
            {
                if (municipiobuscar == null)
                {
                    var listItems= (from m in bd.Municipios
                                    join e in bd.Entidads
                                    on m.IDENTIDAD
                                    equals e.IDENTIDAD
                                    join r in bd.Regions
                                    on m.IDREGION
                                    equals r.IDREGION
                                    orderby m.MUNICIPIO1
                                    select new MunicipioCLS
                                    {
                                        Municipio = m.MUNICIPIO1,
                                        Entidad = e.ENTIDAD1,
                                        Region = r.REGION1
                                    }).ToList();
                    return PartialView("_ListaMunicipio", listItems.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    var listItems = (from m in bd.Municipios
                                     join e in bd.Entidads
                                     on m.IDENTIDAD
                                     equals e.IDENTIDAD
                                     join r in bd.Regions
                                     on m.IDREGION
                                     equals r.IDREGION
                                     where m.MUNICIPIO1.Contains(municipiobuscar)
                                     select new MunicipioCLS
                                     {
                                         Municipio = m.MUNICIPIO1,
                                         Entidad = e.ENTIDAD1,
                                         Region = r.REGION1
                                     }).ToList();
                    listItems = listItems.OrderBy(m => m.Municipio).ToList();
                    return PartialView("_ListaMunicipio", listItems.ToPagedList(pageNumber, pageSize));
                }
            }
        }
        #region Insert

        public string GuardarEntidad(string ent)
        {

            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        int cant = 0;
                        cant = bd.Entidads.Where(r => r.ENTIDAD1 == ent).Count();
                        if (cant.Equals(1))
                        {
                            rpta = "0";
                        }
                        else
                        {
                            Entidad entidad = new Entidad();
                            entidad.IDENTIDAD = bd.Entidads.Max(e => e.IDENTIDAD) + 1;
                            entidad.ENTIDAD1 = ent;
                            bd.Entidads.Add(entidad);
                            rpta = bd.SaveChanges().ToString();
                            transaccion.Complete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return rpta = "error: " + ex;
            }
            return rpta;
        }

        public string GuardarMunicipio(MunicipioCLS municipioCLS)
        {
            string rpta = "";
            
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        int cant = 0;
                        cant = bd.Municipios.Where(m => m.MUNICIPIO1.Contains(municipioCLS.Municipio)).Count();
                        if (cant.Equals(1))
                        {
                            rpta = "Ya existe";
                        }
                        else
                        {
                            if (!ModelState.IsValid)
                            {
                                var query = (from state in ModelState.Values
                                             from error in state.Errors
                                             select error.ErrorMessage).ToList();
                                //forma el html para pasarlo a la vista
                               
                                //los resultados de query se agregaran con un for each
                                foreach (var item in query)
                                {
                                    rpta +=  " "+item + "!!";
                                }
                             
                            }
                            else
                            {
                                Municipio municipio = new Municipio();
                                municipio.MUNICIPIO1 = municipioCLS.Municipio;
                                municipio.IDREGION = municipioCLS.IdRegion;
                                municipio.IDENTIDAD = municipioCLS.IdEntidad;
                                bd.Municipios.Add(municipio);
                                rpta = bd.SaveChanges().ToString();
                                transaccion.Complete();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "error" + ex;
            }
            return rpta;
        }

        #endregion
    }
}