using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Transactions;
using ConcursosContratos.Models;
using System.Data.Entity.Core.Common.CommandTrees;

namespace ConcursosContratos.Controllers
{
    public class OficioAutController : Controller
    {
        // GET: OficioAut
        public ViewResult Index(int? page)
        {
            listaAutoriza();
            listaPrograma();
       
            
            using (var bd = new CCDevEntities())
            {
                var listaOficioAut = (from oa in bd.OficiosAuts
                                      join a in bd.Autorizas on
                                      oa.IDAUTORIZA equals
                                      a.IDAUTORIZA
                                      join p in bd.Programas on
                                      oa.IDPROGRAMA equals
                                      p.IDPROGRAMA
                                      join m in bd.Municipios on
                                      oa.IDMUNICIPIO equals
                                      m.IDMUNICIPIO
                                      select new OficiosAutCLS
                                      {
                                          OficioAut = oa.OFICIOAUT,
                                          FecAutorizacion = oa.FECAUTORIZACION,
                                          FecRecibido = oa.FECRECIBIDO,
                                          NumAsignacion = oa.NUMASIGNACION,
                                          NumObra = oa.NUMOBRA,
                                          DescObra = oa.DESCOBRA,
                                          Autorizado = a.AUTORIZA1,
                                          MontoAutorizado = oa.MONTOAUTORIZADO,
                                          Progamado = p.PROGRAMA1,
                                          Muni = m.MUNICIPIO1
                                      }).ToList();        
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(listaOficioAut.ToPagedList(pageNumber, pageSize));
            }
        }

    ///Inicio Combos
    ///
    ///
    ///
    public void listaAutoriza()
    {
        List<SelectListItem> listItems;
        using (var bd = new CCDevEntities())
        {
            listItems=(from item in bd.Autorizas
                      select new SelectListItem
                      {
                          Text = item.AUTORIZA1,
                          Value = item.IDAUTORIZA.ToString()
                      }).ToList();
            listItems.Insert(0, new SelectListItem { Text = "<--Autorización-->" });
            ViewBag.listaAut = listItems;
        }
    }

    public void listaPrograma()
        {
            List<SelectListItem> listItems;
            using (var bd = new CCDevEntities())
            {
                listItems = (from item in bd.Programas
                             select new SelectListItem
                             {
                                 Text = item.PROGRAMA1,
                                 Value = item.IDPROGRAMA.ToString()
                             }).ToList();
                listItems.Insert(0, new SelectListItem { Text = "<--Programa-->" });
                ViewBag.listaProg = listItems;
            }
        }



        public JsonResult GetMunicipioList( string municipio)
        {
           
            using(var bd= new CCDevEntities())
            {
                var MunicipioList = bd.Regions
                    .Join(bd.Municipios,
                     region => region.IDREGION,
                    m => m.IDREGION,
                      (region, municipio1) => new { municipio1.IDMUNICIPIO, municipio1.MUNICIPIO1, region.IDREGION })
                    .Where(x => x.MUNICIPIO1.Contains(municipio)).ToList();

                return Json(MunicipioList, JsonRequestBehavior.AllowGet);
                    
            }
        }

        //public void listaEstado()
        //{
        //    List<SelectListItem> listItems;
        //    using (var bd = new CCDevEntities())
        //    {
        //        listItems = (from item in bd.Entidads
        //                     select new SelectListItem
        //                     {
        //                         Text = item.ENTIDAD1,
        //                         Value = item.IDENTIDAD.ToString()
        //                     }).ToList();
        //        listItems.Insert(0, new SelectListItem { Text = "<--Seleccionar-->" });
        //        ViewBag.listaEdo = listItems;
        //    }
        //}

        //public JsonResult GetRegionList(int EntidadId)
        //{

        //    using (var bd = new CCDevEntities())
        //    {
        //        //RegionList = bd.Regions.Where(x => x.IDREGION == RegionId).ToList();
        //        var RegionList = bd.Regions
        //            .Join(bd.Municipios,
        //            region => region.IDREGION,
        //            municipio => municipio.IDREGION,
        //           (region, municipio) => new { region.IDREGION, region.REGION1, municipio })
        //        .Where(x => x.municipio.IDENTIDAD == EntidadId)
        //        .GroupBy(x => x.IDREGION)
        //         .Select(x => x.FirstOrDefault()).ToList();

        //        return Json(RegionList, JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}