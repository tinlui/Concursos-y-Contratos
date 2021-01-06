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
                var contOficios = (from item in bd.LicitacionOficioAuts
                                   select new
                                   {
                                       oficioaut = item.IDOFICIOSAUT.ToString()
                                   }).ToList();
                var listaEsp = (from item in bd.OficiosAuts
                                select new
                                {
                                    Text = item.OFICIOAUT,
                                    Value = item.IDOFICIOSAUT.ToString()
                                }).ToList();
                var filtro = listaEsp.Where(w => !contOficios.Any(x => w.Value == x.oficioaut)).ToList();
                if (filtro is null)
                {
                    string rpta = "No Hay Oficios";
                    return Json(rpta,JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(filtro, JsonRequestBehavior.AllowGet);
                }
                //  filtro.Insert(0, new SelectListItem { Text = "<--OFICIO DE AUTORIZACION-->" });

            }
        }

        public JsonResult listaTipoProc()
        {
            using (var bd= new CCDevEntities())
            {
                var listaTp = (from item in bd.Procedimientoes
                               select new SelectListItem
                               {
                                   Text = item.PROCEDIMIENTO1,
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
                listaEspecificacionObra.Insert(0, new SelectListItem { Text = "<--Especificacion Obra-->" });
                return Json(listaEspecificacionObra, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult listarMunicipio()
        {
           
            using (var bd = new CCDevEntities())
            {
            var    listaMun = (from item in bd.Municipios
                            select new SelectListItem
                            {
                                Text = item.MUNICIPIO1,
                                Value = item.IDMUNICIPIO.ToString()
                            }).ToList();
                listaMun.Insert(0, new SelectListItem { Text = "<--Municipio-->" });
                return Json(listaMun, JsonRequestBehavior.AllowGet);
            }
        }
        //direccion
        public JsonResult GetDirecccionList(string dir)
        {
            using (var bd = new CCDevEntities())
            {
                var direccionList = (from item in bd.Direccions
                                     where item.DIRECCION1.Contains(dir)
                                     select new SelectListItem
                                     {
                                         Text = item.DIRECCION1,
                                         Value = item.IDDIRECCION.ToString()
                                     }).ToList();
               if (direccionList.Count == 0)
                {
                    direccionList.Insert(0, new SelectListItem { Text = "No esta registrada" });
                }
                return Json(direccionList, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetIdDireccion(int id)
        {
            using(var bd = new CCDevEntities())
            {
                var idDireccionlst = (from item in bd.Direccions
                                      where item.IDDIRECCION == id
                                      select new SelectListItem
                                      {
                                          Text = item.DIRECCION1,
                                          Value = item.IDDIRECCION.ToString()
                                      }).ToList();
                return Json(idDireccionlst, JsonRequestBehavior.AllowGet);
            }
        }
        //guarda en la tabla datos licitacion
        public string guardaLicitacion(LicitacionCLS licitacionCLS)
        {
            string rpta = "";

            try
            {
                using (var bd = new CCDevEntities())
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
                                rpta += "<li class='list-group-item'>" + item + "</li>";
                            }
                            rpta += "</ul>";
                        }
                        else
                        {
                            int cant = 0;
                            cant = bd.DatosLicitacions.Where(l => l.NOPROCESO.Contains(licitacionCLS.NoProceso)).Count();
                            if (cant.Equals(1))
                            {
                                DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(l => l.NOPROCESO == licitacionCLS.NoProceso).First();
                                datosLicitacion.PLANOS = licitacionCLS.Planos;
                                datosLicitacion.ESPECIFICACIONES = licitacionCLS.Especificaciones;
                                datosLicitacion.NOTASACLARATORIAS = licitacionCLS.Notas;
                                datosLicitacion.IDPROCEDIMIENTO = licitacionCLS.IdProcedimiento;
                                datosLicitacion.AÑO = licitacionCLS.AÑo;
                                datosLicitacion.IDTIPOOBRA = licitacionCLS.IdTipoObra;
                                datosLicitacion.IDNIVELOBRA = licitacionCLS.IdNivelObra;
                                datosLicitacion.IDESPOBRA = licitacionCLS.IdEspObra;
                                datosLicitacion.IDDIRECCION = licitacionCLS.IdDireccion;
                                datosLicitacion.IDSOLICITANTETC = licitacionCLS.IdSolicitante;

                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }
                            else
                            {
                                DatosLicitacion datosLicitacion = new DatosLicitacion();
                                datosLicitacion.NOPROCESO = licitacionCLS.NoProceso;
                                datosLicitacion.PLANOS = licitacionCLS.Planos;
                                datosLicitacion.ESPECIFICACIONES = licitacionCLS.Especificaciones;
                                datosLicitacion.NOTASACLARATORIAS = licitacionCLS.Notas;
                                datosLicitacion.IDPROCEDIMIENTO = licitacionCLS.IdProcedimiento;
                                datosLicitacion.AÑO = licitacionCLS.AÑo;
                                datosLicitacion.IDTIPOOBRA = licitacionCLS.IdTipoObra;
                                datosLicitacion.IDNIVELOBRA = licitacionCLS.IdNivelObra;
                                datosLicitacion.IDESPOBRA = licitacionCLS.IdEspObra;
                                datosLicitacion.IDDIRECCION = licitacionCLS.IdDireccion;
                                datosLicitacion.IDSOLICITANTETC = licitacionCLS.IdSolicitante;

                                bd.DatosLicitacions.Add(datosLicitacion);
                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                            }

                        }
                    }
                                      
                }
            }catch(Exception ex)
            {
               // rpta += "error" + ex.InnerException;
                rpta += "error" + ex.Message;
            }

            return rpta;
        }
     
        public string guardaDireccion(string direccion, int idmun)
        {
            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using(var tran= new TransactionScope())
                    {
                        int cont = 0;
                        cont = bd.Direccions.Where(d => d.DIRECCION1 == direccion && d.IDMUNICIPIO == idmun).Count();
                        if (cont.Equals(1))
                        {
                            rpta = "Ya esta registrado";
                        }
                        else
                        {
                            Direccion direccion1 = new Direccion();
                            direccion1.DIRECCION1 = direccion;
                            direccion1.IDMUNICIPIO = idmun;
                            bd.Direccions.Add(direccion1);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }catch(Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
        
        //inserta en la tabla licitacionofaut
        public string guardaOficioContrato(int oficioaut,string licitacion)
        {
            string rpta = "";

            try { 
                using(var bd = new CCDevEntities())
                {
                    using(var tran = new TransactionScope())
                    {
                        int idLicitacion = 0;
                        DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(dl => dl.NOPROCESO == licitacion).FirstOrDefault();
                        idLicitacion = Convert.ToInt32(datosLicitacion.IDDATOSLICITACION);

                        int contlic = 0;
                        contlic = bd.LicitacionOficioAuts.Where(lo=> lo.IDDATOSLICITACION== idLicitacion && lo.IDOFICIOSAUT== oficioaut).Count();
                        if (contlic.Equals(1))
                        {
                            rpta = "Ya esta vinculado";
                        }
                        else
                        {
                            LicitacionOficioAut licitacionOficioAut = new LicitacionOficioAut();
                            licitacionOficioAut.IDDATOSLICITACION = idLicitacion;
                            licitacionOficioAut.IDOFICIOSAUT = oficioaut;
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
   
        public string guardaAnticipo(AnticipoCls anticipoCls,string noproc)
        {
            string rpta = "";
            try
            {
                using(var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        int idLicitacion = 0;
                        DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(dl => dl.NOPROCESO == noproc).FirstOrDefault();
                        idLicitacion = Convert.ToInt32(datosLicitacion.IDDATOSLICITACION);

                        int connAnt = 0;
                        connAnt = bd.Anticipoes.Where(An => An.IDDATOSLICITACION == idLicitacion).Count();
                        if (connAnt.Equals(1)) {
                            Anticipo anticipo = bd.Anticipoes.Where(a => a.IDDATOSLICITACION == idLicitacion).FirstOrDefault();
                            anticipo.ANTICIPO1 = anticipoCls.Anticipo;
                            anticipo.ANTICIPOINICIO = anticipoCls.AnticipoInicio;
                            anticipo.ANTICIPOMATERIALES = anticipoCls.AnticipoMateriales;
                            anticipo.IVA = anticipoCls.Iva;
                            anticipo.CAPITALMINIMO = anticipoCls.CapitalMinimo;
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        } else {
                            Anticipo anticipo = new Anticipo();
                            anticipo.IDDATOSLICITACION = idLicitacion;
                            anticipo.ANTICIPO1 = anticipoCls.Anticipo;
                            anticipo.ANTICIPOINICIO = anticipoCls.AnticipoInicio;
                            anticipo.ANTICIPOMATERIALES = anticipoCls.AnticipoMateriales;
                            anticipo.IVA = anticipoCls.Iva;
                            anticipo.CAPITALMINIMO = anticipoCls.CapitalMinimo;

                            bd.Anticipoes.Add(anticipo);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }catch(Exception ex)
            {
                rpta = "error " + ex.Message;
            }
            return rpta;
        }
        public string VerificaNoProceso(string noproc)
        {
            string rpta = "";
            int numProceso = 0;

            using (var bd = new CCDevEntities())
            {
               numProceso= bd.DatosLicitacions.Where(dl => dl.NOPROCESO == noproc).Count();
                if(numProceso.Equals(1))
                {
                    rpta = "1";
                }
                else
                {
                    rpta = "0";
                }
            }
            return rpta;
        }
        public string guardaConvocatoria(ConvocatoriaCls convocatoriaCls, string noproc)
        {
            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        int idlic = 0;
                        DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(dl => dl.NOPROCESO == noproc).FirstOrDefault();
                        idlic = Convert.ToInt32(datosLicitacion.IDDATOSLICITACION);

                        int cont = 0;
                        cont = bd.Convocatorias.Where(co => co.IDDATOSLICITACION == idlic).Count();

                        if (cont.Equals(1))
                        {
                            Convocatoria convocatoria = bd.Convocatorias.Where(cn => cn.IDDATOSLICITACION == idlic).FirstOrDefault();
                            convocatoria.PROCESO = convocatoriaCls.Proceso;
                            convocatoria.PROCESOAUT = convocatoriaCls.ProcesoAut;
                            convocatoria.CONVOCATORIADO = convocatoriaCls.ConvocatoriaDo;
                            convocatoria.ENVIOCN = convocatoriaCls.EnvioCn;
                            convocatoria.PUBLICACION = convocatoriaCls.Publicacion;
                            convocatoria.FLIMITE1 = convocatoriaCls.FLimite1;
                            convocatoria.FLIMITE2 = convocatoriaCls.FLimite2;
                            convocatoria.RECEPCIONLICITAR = convocatoriaCls.RecepcionLicitar;
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                        else
                        {
                            Convocatoria convocatoria = new Convocatoria();
                            convocatoria.IDDATOSLICITACION = idlic;
                            convocatoria.PROCESO = convocatoriaCls.Proceso;
                            convocatoria.PROCESOAUT = convocatoriaCls.ProcesoAut;
                            convocatoria.CONVOCATORIADO = convocatoriaCls.ConvocatoriaDo;
                            convocatoria.ENVIOCN = convocatoriaCls.EnvioCn;
                            convocatoria.PUBLICACION = convocatoriaCls.Publicacion;
                            convocatoria.FLIMITE1 = convocatoriaCls.FLimite1;
                            convocatoria.FLIMITE2 = convocatoriaCls.FLimite2;
                            convocatoria.RECEPCIONLICITAR = convocatoriaCls.RecepcionLicitar;

                            bd.Convocatorias.Add(convocatoria);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "error " + ex.Message;
            }
            return rpta;
        }

        public string guardaVisita(VisitaProgCls visitaProgCls, string noproc)
        {
            string rpta = "";
            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var tran = new TransactionScope())
                    {
                        int idlic = 0;
                        DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(dl => dl.NOPROCESO == noproc).FirstOrDefault();
                        idlic = Convert.ToInt32(datosLicitacion.IDDATOSLICITACION);

                        int cont = 0;
                        cont = bd.VisitaProgs.Where(vp => vp.IDDATOSLICITACION == idlic).Count();

                        if (cont.Equals(1))
                        {
                            VisitaProg visitaProg = bd.VisitaProgs.Where(vp => vp.IDDATOSLICITACION == idlic).FirstOrDefault();
                            visitaProg.VISITAF = visitaProgCls.VisitaF;
                            visitaProg.VISITAH = visitaProgCls.VisitaH;
                            visitaProg.IDDIRECCION = visitaProgCls.IdDireccion;
                            visitaProg.JACFECHA = visitaProgCls.JacFecha;
                            visitaProg.JACHORA = visitaProgCls.JacHora;
                            visitaProg.ATECFECHA = visitaProgCls.AtecFecha;
                            visitaProg.ATECHORA = visitaProgCls.AtecHora;
                            visitaProg.ACTFALLAFECHA = visitaProgCls.ActaFallaFecha;
                            visitaProg.ACTFALLAHORA = visitaProgCls.ActaFallaHora;
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                        else
                        {
                            VisitaProg visitaProg = new VisitaProg();
                            visitaProg.IDDATOSLICITACION = idlic;
                            visitaProg.VISITAF = visitaProgCls.VisitaF;
                            visitaProg.VISITAH = visitaProgCls.VisitaH;
                            visitaProg.IDDIRECCION = visitaProgCls.IdDireccion;
                            visitaProg.JACFECHA = visitaProgCls.JacFecha;
                            visitaProg.JACHORA = visitaProgCls.JacHora;
                            visitaProg.ATECFECHA = visitaProgCls.AtecFecha;
                            visitaProg.ATECHORA = visitaProgCls.AtecHora;
                            visitaProg.ACTFALLAFECHA = visitaProgCls.ActaFallaFecha;
                            visitaProg.ACTFALLAHORA = visitaProgCls.ActaFallaHora;

                            bd.VisitaProgs.Add(visitaProg);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "error " + ex.Message;
            }
            return rpta;
        }

        //llena la tabla de oficios asignados al contrato
        public JsonResult licitacionOficioAut(string noProceso)

        {
            object listaOfAut = null;
            int idDt = 0;
            using (var bd = new CCDevEntities())
            {

                DatosLicitacion datosLicitacion = bd.DatosLicitacions.Where(m => m.NOPROCESO == noProceso).FirstOrDefault();
                if (datosLicitacion != null)
                {
                    idDt = datosLicitacion.IDDATOSLICITACION;

                    listaOfAut = (from item in bd.LicitacionOficioAuts
                                  join oa in bd.OficiosAuts on
                                  item.IDOFICIOSAUT equals oa.IDOFICIOSAUT
                                  where item.IDDATOSLICITACION == idDt
                                  select new
                                  { 
                                      Oficio = oa.OFICIOAUT,
                                      MontoAutorizado = oa.MONTOAUTORIZADO.ToString()
                                  }).ToList();
                }
                             
                return Json(listaOfAut, JsonRequestBehavior.AllowGet);
            }
        }
        // LLena Campos
        [HttpGet]
        public JsonResult Recuperar(int id)
        {
            string rpta = "";
            using (var bd = new CCDevEntities())
            {
                if (id != 0) {
                    List<LicitacionCLS> listaDL = (
                                            from dl in bd.DatosLicitacions
                                            where dl.IDDATOSLICITACION == id
                                            select new LicitacionCLS
                                            {
                                                IdDatosLicitacion = (int)dl.IDDATOSLICITACION,
                                                NoProceso = dl.NOPROCESO,
                                                Planos = (int)dl.PLANOS,
                                                Especificaciones = (int)dl.ESPECIFICACIONES,
                                                Notas = dl.NOTASACLARATORIAS,
                                                IdProcedimiento = (int)dl.IDPROCEDIMIENTO,
                                                AÑo = (int)dl.AÑO,
                                                IdTipoObra = (int)dl.IDTIPOOBRA,
                                                IdNivelObra = (int)dl.IDNIVELOBRA,
                                                IdEspObra = (int)dl.IDESPOBRA,
                                                IdDireccion = (int)dl.IDDIRECCION,
                                                IdSolicitante = (int)dl.IDSOLICITANTETC
                                            }).ToList();
                    return Json(listaDL, JsonRequestBehavior.AllowGet);
                } else
                {
                    rpta = "no data";
                    return Json(rpta, JsonRequestBehavior.AllowGet);
                }
            
            }
           
        }

        public JsonResult RecAnticipo(int id)
        {
            string rpta = "";
            List<AnticipoCls> listAnticipo=null;
            if (id != 0)
            {
                using(var bd= new CCDevEntities())
                {
                    listAnticipo = (
                                    from an in bd.Anticipoes
                                    where an.IDDATOSLICITACION == id
                                    select new AnticipoCls
                                    {
                                        Anticipo=(decimal)an.ANTICIPO1,
                                        AnticipoInicio=(decimal)an.ANTICIPOINICIO,
                                        AnticipoMateriales=(decimal)an.ANTICIPOMATERIALES,
                                        Iva=(decimal)an.IVA,
                                        CapitalMinimo=(decimal)an.CAPITALMINIMO
                                    }).ToList();
                }
                return Json(listAnticipo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                rpta = "no data";
                return Json(rpta, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RecConvocatoria(int id)
        {
            string rpta = "";
            List<ConvocatoriaCls> listConvocatoria;
            if (id != 0)
            {
               using(var bd = new CCDevEntities())
                {
                    listConvocatoria = (
                                        from co in bd.Convocatorias
                                        where co.IDDATOSLICITACION == id
                                        select new ConvocatoriaCls
                                        {
                                            Proceso= (DateTime)co.PROCESO,
                                            ProcesoAut= (DateTime)co.PROCESOAUT,
                                            ConvocatoriaDo= (DateTime)co.CONVOCATORIADO,
                                            EnvioCn= (DateTime)co.ENVIOCN,
                                            Publicacion= (DateTime)co.PUBLICACION,
                                            FLimite1= (DateTime)co.FLIMITE1,
                                            FLimite2= (DateTime)co.FLIMITE2,
                                            RecepcionLicitar= (DateTime)co.RECEPCIONLICITAR
                                        }).ToList();
                }
                return Json(listConvocatoria, JsonRequestBehavior.AllowGet);
            }
            else
            {
                rpta = "no data";
                return Json(rpta, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult RecVisita(int id)
        {
            string rpta = "";
            List<VisitaProgCls> listVisita;
            if (id != 0)
            {
                using(var bd = new CCDevEntities())
                {
                    listVisita = (
                                from vi in bd.VisitaProgs
                                where vi.IDDATOSLICITACION == id
                                select new VisitaProgCls
                                {
                                    VisitaF= (DateTime)vi.VISITAF,
                                    VisitaH=vi.VISITAH,
                                    IdDireccion= (int)vi.IDDIRECCION,
                                    JacFecha= (DateTime)vi.JACFECHA,
                                    JacHora=vi.JACHORA,
                                    AtecFecha= (DateTime)vi.ATECFECHA,
                                    AtecHora=vi.ATECHORA,
                                    ActaFallaFecha=(DateTime)vi.ACTFALLAFECHA,
                                    ActaFallaHora=vi.ACTFALLAHORA
                                }
                        ).ToList();
                }
                return Json(listVisita, JsonRequestBehavior.AllowGet);
            }
            else
            {
                rpta = "no data";
                return Json(rpta, JsonRequestBehavior.AllowGet);
            }
        }
    }
}