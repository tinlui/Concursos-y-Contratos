using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConcursosContratos.Controllers
{
    public class PaginaPrincipalController : Controller
    {
        // GET: PaginaPrincipal
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult listLicitaciones()

        {
            object listaLicitaciones = null;
            
            using (var bd = new CCDevEntities())
            {

                listaLicitaciones = (from dl in bd.DatosLicitacions
                                  join p in bd.Procedimientoes on
                                  dl.IDPROCEDIMIENTO equals p.IDPROCEDIMIENTO
                                 join tp in bd.TipoObras on
                                 dl.IDTIPOOBRA equals tp.IDTIPOOBRA
                                 join nob in bd.NivelObras on
                                 dl.IDNIVELOBRA equals nob.IDNIVELOBRA
                                 join eo in bd.EspObras on
                                 dl.IDESPOBRA equals eo.IDESPOBRA
                                 join tc in bd.TipoContratoes on
                                 dl.IDSOLICITANTETC equals tc.IDTIPOCONTRATO
                                  select new
                                  {
                                      id=dl.IDDATOSLICITACION,
                                      noProceso=dl.NOPROCESO,
                                      planos=dl.PLANOS,
                                      especificaciones=dl.ESPECIFICACIONES,
                                      abvProc=p.ABVPROC,
                                      año=dl.AÑO,
                                      tipoObra=tp.TIPOOBRA1,
                                      nvlObra=nob.NIVELOBRA1,
                                      espObra=eo.ESPOBRA1,
                                      tipoContrato=tc.TIPOCONTRATO1
                                  }).ToList();
                return Json(listaLicitaciones, JsonRequestBehavior.AllowGet);
            }
        }

    }
}