using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Transactions;
using ConcursosContratos.Models;

using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ConcursosContratos.Controllers
{
    public class OficioAutController : Controller
    {
        // GET: OficioAut
        public ViewResult Index(int? page)
        {
            listaAutoriza();
            listaPrograma();
            listaFuenteFin();
         

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
                                          FecAutorizacion = oa.AUTORIZACION,
                                          FecRecibido = oa.RECIBIDO,
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

        /// <FuenteFinanciamiento>
        /// 
        public void listaFuenteFin()
        {
            List<SelectListItem> listItems;
            using (var bd = new CCDevEntities())
            {
                listItems = (from item in bd.FuenteFins
                             select new SelectListItem
                             {
                                 Text = item.FUENTEFIN1,
                                 Value = item.IDFUENTEFIN.ToString()
                             }).ToList();
                listItems.Insert(0, new SelectListItem { Text = "<--Financiamiento-->" });
                ViewBag.listaFuenteFin = listItems;
            }
        }
        public JsonResult ShowOrigen(int idFuenteFin)
        {
            MontoFinCLS montoFinCLS = new MontoFinCLS();
            
            using (var bd = new CCDevEntities())
            {
                FuenteFin fuenteFin = bd.FuenteFins.Where(f => f.IDFUENTEFIN == idFuenteFin).First();
                int idest = Convert.ToInt32(fuenteFin.IDESTRUCTURAFIN);

                EstructuraFin estructuraFin = bd.EstructuraFins.Where(ef => ef.IDESTRUCTURAFIN == idest).First();
                montoFinCLS.IdEstructuraFin = estructuraFin.IDESTRUCTURAFIN;
                montoFinCLS.EstructuraFin = estructuraFin.ESTRUCTURAFIN1;
            }
            return Json(montoFinCLS, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Oficio(string numOficio)
        {
            int noOficio = 0;
            using (var bd = new CCDevEntities())
            {
                OficiosAut oficiosAut = bd.OficiosAuts.Where(m => m.OFICIOAUT == numOficio).First();
                noOficio = oficiosAut.IDOFICIOSAUT;
            }
            return Json(noOficio, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MontoxOficio(int oficioId)
        {
            var montoxOficio = new List<MontoFinCLS>();
            using(var bd= new CCDevEntities())
            {
                montoxOficio = (from mf in bd.MontoFins
                                join ff in bd.FuenteFins
                                on mf.IDFUENTEFIN
                                equals ff.IDFUENTEFIN
                                join ef in bd.EstructuraFins
                                on ff.IDESTRUCTURAFIN
                                equals ef.IDESTRUCTURAFIN
                                where mf.IDOFICIOSAUT == oficioId
                                select new MontoFinCLS { 
                                    FuenteFin=ff.FUENTEFIN1,
                                   EstructuraFin=ef.ESTRUCTURAFIN1,
                                    Monto=mf.MONTO
                              
                                }).ToList();
            }
            return Json( montoxOficio,JsonRequestBehavior.AllowGet);
        }
        /// </summary>
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
        #region Insert
        public string guardarOficio(OficiosAutCLS oficiosAutCLS)
        {
            
            string rpta = "";
            try
            {
                using(var bd= new CCDevEntities())
                {
                    using(var tran = new TransactionScope())
                    {
                        int cant = 0;
                        cant = bd.OficiosAuts.Where(o => o.OFICIOAUT.Contains(oficiosAutCLS.OficioAut)).Count();

                        if (cant.Equals(1))
                        {
                            rpta = "ya existe!";
                        }
                        else
                        {
                            if (!ModelState.IsValid)
                            {
                                var query = (from state in ModelState.Values
                                             from error in state.Errors
                                             select error.ErrorMessage).ToList();
                                foreach (var item in query)
                                {
                                    rpta += " " + item + "!!";
                                }
                            }
                            else
                            {
                                OficiosAut oficiosAut = new OficiosAut();
                              
                                oficiosAut.OFICIOAUT = oficiosAutCLS.OficioAut;
                                oficiosAut.AUTORIZACION = oficiosAutCLS.FecAutorizacion;
                                oficiosAut.RECIBIDO = oficiosAutCLS.FecRecibido;
                                oficiosAut.NUMASIGNACION = oficiosAutCLS.NumAsignacion;
                                oficiosAut.NUMOBRA = oficiosAutCLS.NumObra;
                                oficiosAut.DESCOBRA = oficiosAutCLS.DescObra;
                                oficiosAut.IDAUTORIZA = oficiosAutCLS.IdAutoriza;
                                oficiosAut.MONTOAUTORIZADO = oficiosAutCLS.MontoAutorizado;
                                oficiosAut.IDPROGRAMA = oficiosAutCLS.IdPrograma;
                                oficiosAut.IDMUNICIPIO = oficiosAutCLS.IdMunicipio;
                                bd.OficiosAuts.Add(oficiosAut);

                                rpta = bd.SaveChanges().ToString();
                                tran.Complete();
                               
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                rpta = "error" + ex.Message;
            }
            return rpta;
        }


        public string guardarMonto(MontoFinCLS montoFinCLS)
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
                            //forma el html para pasarlo a la vista

                            //los resultados de query se agregaran con un for each
                            foreach (var item in query)
                            {
                                rpta += " " + item + "!!";
                            }

                        }
                        else
                        {
                            MontoFin montoFin = new MontoFin();
                          
                            montoFin.IDOFICIOSAUT = montoFinCLS.IdOficioSAut;
                            montoFin.IDFUENTEFIN = montoFinCLS.IdFuenteFin;
                            montoFin.MONTO = montoFinCLS.Monto;

                            bd.MontoFins.Add(montoFin);
                            rpta = bd.SaveChanges().ToString();
                            tran.Complete();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                rpta = "error" + ex.Message;
            }
            return rpta;
        }
        #endregion
        #region Cargas Masivas
        [HttpPost]
        public ActionResult CargaMasivaUp(HttpPostedFileBase postedFile)
        {
          
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[11] { new DataColumn("IdOficio", typeof(int)),
                    new DataColumn("oficioAut", typeof(int)),
                                new DataColumn("fecAut", typeof(DateTime)),
                                new DataColumn("fecRec",typeof(DateTime)),
                                new DataColumn("numAsig",typeof(int)),
                                new DataColumn("numObra",typeof(int)),
                                new DataColumn("descObra",typeof(string)),
                                new DataColumn("idAut",typeof(int)),
                                new DataColumn("montoAut",typeof(decimal)),
                                new DataColumn("idProg",typeof(int)),
                                     new DataColumn("idMunicipio",typeof(int))

                });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {

                        try
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                        catch (Exception ex){
                            ViewBag.mensaje = ex.Message;
                            return PartialView("_CargaMasiva"); 
                        }
                       


                    }
                }

                string conString = ConfigurationManager.ConnectionStrings["CCDMasivo"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "financiamento.OficiosAut";
                        try
                        {
                            //[OPTIONAL]: Map the DataTable columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("IdOficio", "IDOFICIOSAUT");
                            sqlBulkCopy.ColumnMappings.Add("OficioAut", "OFICIOAUT");
                            sqlBulkCopy.ColumnMappings.Add("fecAut", "FECAUTORIZACION");
                            sqlBulkCopy.ColumnMappings.Add("fecRec", "FECRECIBIDO");
                            sqlBulkCopy.ColumnMappings.Add("numAsig", "NUMASIGNACION");
                            sqlBulkCopy.ColumnMappings.Add("numObra", "NUMOBRA");
                            sqlBulkCopy.ColumnMappings.Add("descObra", "DESCOBRA");
                            sqlBulkCopy.ColumnMappings.Add("idAut", "IDAUTORIZA");
                            sqlBulkCopy.ColumnMappings.Add("montoAut", "MONTOAUTORIZADO");
                            sqlBulkCopy.ColumnMappings.Add("idProg", "IDPROGRAMA");
                            sqlBulkCopy.ColumnMappings.Add("idMunicipio", "IDMUNICIPIO");


                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                            string msg = "Guardado";
                            ViewBag.mensaje = msg;
                            return PartialView("_CargaMasiva");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.mensaje = ex.Message;
                            return PartialView("_CargaMasiva");
                        }
                      
                      
                    }
                }
            }
            return PartialView("_CargaMasiva");
        }
        [HttpPost]
        public ActionResult CargaMasivaMup(HttpPostedFileBase postedFile)
        {

            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);

                //Create a DataTable.
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[5] { new DataColumn("IdMontoFin", typeof(int)),
                    new DataColumn("IdOficiosAut", typeof(int)),
                                new DataColumn("IdFuenteFin", typeof(int)),
                                new DataColumn("IdEstructuraFin",typeof(int)),
                                new DataColumn("Monto",typeof(int))

                });


                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {

                        try
                        {
                            dt.Rows.Add();
                            int i = 0;

                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;

                            }
                        }
                        catch (Exception ex)
                        {
                            ViewBag.mensaje = ex.Message;
                            return PartialView("_CargaMasivaMonto");
                        }



                    }
                }

                string conString = ConfigurationManager.ConnectionStrings["CCDMasivo"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "financiamento.MontoFin";
                        try
                        {
                            //[OPTIONAL]: Map the DataTable columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("IdMontoFin", "IDMONTOFIN");
                            sqlBulkCopy.ColumnMappings.Add("IdOficiosAut", "IDOFICIOSAUT");
                            sqlBulkCopy.ColumnMappings.Add("IdFuenteFin", "IDFUENTEFIN");
                            sqlBulkCopy.ColumnMappings.Add("IdEstructuraFin", "IDESTRUCTURFIN");
                            sqlBulkCopy.ColumnMappings.Add("Monto", "MONTO");


                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                            string msg = "Guardado";
                            ViewBag.mensaje = msg;
                            return PartialView("_CargaMasivaMonto");
                        }
                        catch (Exception ex)
                        {
                            ViewBag.mensaje = ex.Message;
                            return PartialView("_CargaMasivaMonto");
                        }


                    }
                }
            }
            return PartialView("_CargaMasivaMonto");
        }
        #endregion
    }
}