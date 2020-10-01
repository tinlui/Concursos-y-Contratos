using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace ConcursosContratos.Controllers
{
    public class LicitacionController : Controller
    {
        // GET: Licitacion
        public ActionResult Index()
        {
            
            return View();
        }
        //llena el dropdown
        public JsonResult listaOfAutoriza()
        {
            using (var bd = new CCDevEntities())
            {
                var listaEsp = (from item in bd.OficiosAuts
                                select new SelectListItem
                                {
                                    Text = item.OFICIOAUT,
                                    Value = item.IDOFICIOSAUT.ToString()
                                }).ToList();
                listaEsp.Insert(0, new SelectListItem { Text = "<--OFICIO DE AUTORIZACION-->" });
                return Json(listaEsp, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaTipoProc()
        {
            using (var bd= new CCDevEntities())
            {
                var listaTp = (from item in bd.Procedimientoes
                               select new SelectListItem
                               {
                                   Text = item.ABVPROC,
                                   Value = item.IDPROCEDIMIENTO.ToString()
                               }).ToList();
                listaTp.Insert(0, new SelectListItem { Text = "<--Procedimiento-->" });
                return Json(listaTp, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaTipoCont()
        {
            using(var bd= new CCDevEntities())
            {
                var litsaTcont = (from item in bd.TipoContratoes
                                  select new SelectListItem
                                  {
                                      Text = item.TIPOCONTRATO1+" | " + item.DESCRIPCION,
                                      Value = item.IDTIPOCONTRATO.ToString()
                                  }).ToList();
                litsaTcont.Insert(0, new SelectListItem { Text = "<--Solicitante-->" });
                return Json(litsaTcont, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaNivelObra()
        {
            using (var bd = new CCDevEntities())
            {
                var listaNivObra = (from item in bd.NivelObras
                                    select new SelectListItem
                                    {
                                        Text = item.NIVELOBRA1,
                                        Value = item.IDNIVELOBRA.ToString()
                                    }).ToList();
                listaNivObra.Insert(0, new SelectListItem { Text = "<--Nivel Obra-->" });
                return Json(listaNivObra, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaTipoObra()
        {
            using(var bd = new CCDevEntities())
            {
                var listaTipoOb = (from item in bd.TipoObras
                                   select new SelectListItem
                                   {
                                       Text = item.TIPOOBRA1,
                                       Value = item.IDTIPOOBRA.ToString()
                                   }).ToList();
                listaTipoOb.Insert(0, new SelectListItem { Text = "<--Tipo Obra-->" });
                return Json(listaTipoOb, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult listaEspObra()
        {
            using(var bd = new CCDevEntities())
            {
                var listaEspecificacionObra = (from item in bd.EspObras
                                               select new SelectListItem
                                               {
                                                   Text = item.ESPOBRA1,
                                                   Value = item.IDESPOBRA.ToString()
                                               }).ToList();
                listaEspecificacionObra.Insert(0, new SelectListItem { Text = "<--Tipo Obra-->" });
                return Json(listaEspecificacionObra, JsonRequestBehavior.AllowGet);
            }
        }
        //inserta en la tabla licitacionofaut
        public string guardaOficioContrato(string oficioaut,int licitacion)
        {
            string rpta = "";

            try { 
                using(var bd = new CCDevEntities())
                {
                    using(var tran = new TransactionScope())
                    {
                        int idoficio = 0;
                        OficiosAut oficiosAut = bd.OficiosAuts.Where(oa => oa.OFICIOAUT.Contains(oficioaut)).First();
                        idoficio = Convert.ToInt32(oficiosAut.IDOFICIOSAUT);
                        
                        int contlic = 0;
                        contlic = bd.LicitacionOficioAuts.Where(lo=> lo.IDDATOSLICITACION.Equals(licitacion) && lo.IDOFICIOSAUT.Equals(idoficio)).Count();
                        if (contlic.Equals(1))
                        {
                            rpta = "Ya esta vinculado";
                        }
                        else
                        {
                            LicitacionOficioAut licitacionOficioAut = new LicitacionOficioAut();
                            licitacionOficioAut.IDDATOSLICITACION = licitacion;
                            licitacionOficioAut.IDOFICIOSAUT = idoficio;
                            bd.LicitacionOficioAuts.Add(licitacionOficioAut);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }catch (Exception ex){
                rpta = "error" + ex.Message;
            }

            return rpta;
        }
        //llena la tabla de oficios asignados al contrato
        public JsonResult licitacionOficioAut(int idContrato)

        {
            int idDt = 0;
            using (var bd = new CCDevEntities())
            {
                DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(m => m.IDCONTRATO == idContrato).First();
                idDt = datosLicitacion.IDDATOSLICITACION;

                var listaOfAut = (from item in bd.LicitacionOficioAuts
                                  join oa in bd.OficiosAuts on
                                  item.IDOFICIOSAUT equals oa.IDOFICIOSAUT
                                  where item.IDDATOSLICITACION == idDt
                                  select new SelectListItem
                                  {
                                      Text=oa.OFICIOAUT,
                                      Value=item.IDLICOFAUT.ToString()
                                  }).ToList();
                return Json(listaOfAut, JsonRequestBehavior.AllowGet);
            }
        }
      
    }
}