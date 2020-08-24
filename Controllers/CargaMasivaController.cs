using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ConcursosContratos.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            string msg = "";
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
                        dt.Rows.Add();
                        int i = 0;
                      
                            //Execute a loop over the columns.
                            foreach (string cell in row.Split(','))
                            {
                            if (cell == "") {
                                msg="No puede haber campos vacios";
                                break;
                            } else
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell;
                                i++;
                            }
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
                        ViewBag.Message = "Archivo cargado exitosamente!!";
                    }
                }
            }

            ViewBag.Message(msg);
            return View();
        }
    }
}