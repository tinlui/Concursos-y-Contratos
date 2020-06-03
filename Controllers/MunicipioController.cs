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

        public void listaEntidad()
        {
            List<SelectListItem> listItems;
            using (var bd = new CCDevEntities())
            {
                listItems = (from item in bd.Entidads
                             select new SelectListItem
                             {
                                 Text = item.ENTIDAD1,
                                 Value = item.IDENTIDAD.ToString()
                             }).ToList();
                listItems.Insert(0, new SelectListItem { Text = "<--Entidad-->" });
                ViewBag.listaEntidad = listItems;
            }
        }

        public void listaRegion()
        {
            List<SelectListItem> listItems;
            using (var bd = new CCDevEntities())
            {

                listItems = (from item in bd.Regions
                             select new SelectListItem
                             {
                                 Text = item.REGION1,
                                 Value = item.IDREGION.ToString()
                             }).ToList();
                listItems.Insert(0, new SelectListItem { Text = "<--Region-->" });
                ViewBag.listaRegion = listItems;
            }
        }

        // GET: Municipio
        public ViewResult Index(int? page)
        {
            listaEntidad();
            listaRegion();
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
            listaEntidad();
            listaRegion();
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
    }
}