using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Web.UI;
using ConcursosContratos.Models;
using System.Text;
using System.Net;
using iTextSharp.text.xml;
using System.Drawing;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
using System.Security.Cryptography;
using System.Configuration;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web.Hosting;

namespace ConcursosContratos.Controllers
{
    public class GeneradorPdfController : Controller
    {
        // GET: GeneradorPdf
        public ActionResult Index()
        {
            return View();
        }
        
        public FileResult PdfContratista(int contratista)
        {
         
            int idcont = 0;
            idcont = contratista;
            Document doc = new Document();
            byte[] buffer;

            using(MemoryStream ms= new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                //imagen
                //----------------------------------------------------//
                //titulo
                Paragraph title = new Paragraph("Contratista");
                title.Alignment = Element.ALIGN_CENTER;
                doc.Add(title);
                //----------------------------------------------------//
                Paragraph espacio = new Paragraph(" ");
                doc.Add(espacio);

                ///----------------------------------------------------//
                ///
                ///General
                ///
                ///----------------------------------------------------//
                ///
             
                string[] modelos = { "Contratista", "Moral","Registro","Poder","Representa" };
                foreach(string el in modelos)
                {
                   //si existe el id en el modelo

                    //llena la tabla con el modelo y el id
                    PdfPTable tabla = existeDatos(idcont, el);
                    doc.Add(tabla);
                    
                    PdfPTable table1 = fillitems(idcont, el);
                    doc.Add(table1);
                    Paragraph espacio2 = new Paragraph(" ");
                    doc.Add(espacio2);

                }           
                doc.Close();
                buffer = ms.ToArray();
            }

            return File( buffer,"application/pdf");
        }

        public PdfPTable fillitems(int idcontratista, string model)
        {
            PdfPTable table=null;
            List<ContratistaCLS> elements = null;
           int itemList = 0;
            string nombreAtt = "";
            string nombre = "";
            string valor = null;

            using (var bd = new CCDevEntities())
            {
                if (model == "Contratista")
                {

                    elements = (from c in bd.Contratistas
                                join m in bd.Municipios
                                on c.IDMUNICIPIO equals m.IDMUNICIPIO
                                join ent in bd.Entidads
                                on m.IDENTIDAD equals ent.IDENTIDAD
                                join reg in bd.Regions
                                on m.IDREGION equals reg.IDREGION
                                where c.IDCONTRATISTA.Equals(idcontratista)
                                select new ContratistaCLS
                                {
                                    Nombre = c.NOMBRE,
                                    Rfc = c.RFC,
                                    Curp = c.CURP,
                                    Telefono = c.TELEFONO,
                                    Correo = c.CORREO,
                                    Calle = c.CALLE,
                                    NoExterior = c.NOEXTERIOR,
                                    NoInterior = c.NOINTERIOR,
                                    Colonia=c.COLONIA,
                                    Año=(int)c.AÑO,
                                    Cp = (int)c.CP,
                                    municipio = m.MUNICIPIO1,
                                    entidad = ent.ENTIDAD1,
                                    region = reg.REGION1
                                }).ToList();
 
                PropertyInfo[] lst = typeof(ContratistaCLS).GetProperties();
                    int tam = lst.Length;
                    table = new PdfPTable(6);
                    foreach (PropertyInfo propertyInfo in lst)
                {
                        nombreAtt = propertyInfo.Name;
                        //validacion de campos que no iran en el reporte
                        if (nombreAtt != "IdContratista" && nombreAtt!="Fax" && nombreAtt!="IdMunicipio")
                    {
                          ///si no tiene datos le ponemos vacio
                            if (propertyInfo.GetValue(elements[0]) == null)
                            {
                                nombre = nombreAtt;
                                valor = "";
                                itemList++;

                            }
                            else
                            {
                                nombre = nombreAtt;
                                valor = propertyInfo.GetValue(elements[0]).ToString();
                                itemList++;
                                //encabezados
                                if (nombreAtt == "Nombre")
                                {
                                    PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
                                    {
                                        BackgroundColor = new BaseColor(255, 153, 153),
                                        HorizontalAlignment = PdfPCell.ALIGN_LEFT,
                                        
                                        

                                    };
                                    table.AddCell(head);
                                    //datos
                                    PdfPCell text = new PdfPCell(new Phrase(valor))
                                    {
                                        BackgroundColor = new BaseColor(255, 255, 255),
                                        HorizontalAlignment = PdfPCell.ALIGN_CENTER,
                                        Colspan = 3
                                    };
                                    table.AddCell(text);
                                }
                                else if (nombreAtt == "Calle" || nombreAtt == "Correo")
                                {
                                    PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
                                    {
                                        BackgroundColor = new BaseColor(255, 153, 153),
                                        HorizontalAlignment = PdfPCell.ALIGN_LEFT
                                    };
                                    table.AddCell(head);
                                    //datos
                                    PdfPCell text = new PdfPCell(new Phrase(valor))
                                    {
                                        BackgroundColor = new BaseColor(255, 255, 255),
                                        HorizontalAlignment = PdfPCell.ALIGN_CENTER,
                                        Colspan = 3
                                    };
                                    table.AddCell(text);
                                }
                                else
                                {
                                    PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
                                    {
                                        BackgroundColor = new BaseColor(255, 153, 153),
                                        HorizontalAlignment = PdfPCell.ALIGN_LEFT
                                    };
                                    table.AddCell(head);
                                    //datos
                                    PdfPCell text = new PdfPCell(new Phrase(valor))
                                    {
                                        BackgroundColor = new BaseColor(255, 255, 255),
                                        HorizontalAlignment = PdfPCell.ALIGN_CENTER
                                    };
                                    table.AddCell(text);
                            }


                        }

                        }


                    }

                }
                else
                {
                    table = new PdfPTable(1);
                    float[] values = new float[1] { 80 };
                    table.SetWidths(values);
                    PdfPCell celda1 = new PdfPCell(new Phrase(""));
                    table.AddCell(celda1);
                }                
            }
            return table;
        }

        public PdfPTable existeDatos(int idcontratista,string model)
        {
            int cant = 0;
            int cantR = 0;
            int idpod = 0;
            PdfPTable table = null;
            using (var bd= new CCDevEntities())
            {
                switch (model)
                {
                    case "Contratista":
                        cant = bd.Contratistas.Where(c => c.IDCONTRATISTA.Equals(idcontratista)).Count();
                        break;

                    case "Moral":
                        cant = bd.DatosMorals.Where(m => m.IDCONTRATISTA==idcontratista).Count();
                        break;

                    case "Registro":
                        cant= bd.RegistroContratistas.Where(rc => rc.IDCONTRATISTA==idcontratista).Count();
                        break;

                    case "Representa":
                        cant= bd.DatosRepresentas.Where(r => r.IDCONTRATISTA==idcontratista).Count();
                        break;

                    case "Poder":
                        cantR = bd.DatosRepresentas.Where(r => r.IDCONTRATISTA == idcontratista).Count();
                        
                        idpod = Convert.ToInt32(cantR);
                        cant = bd.DatosPoders.Where(p => p.IDREPRESENTA==idpod).Count();
                        break;
                }
                if (cant == 1)
                {
                     table = new PdfPTable(2);
                    float[] values = new float[2] { 30, 80 };
                    table.SetWidths(values);

                    PdfPCell celda1 = new PdfPCell(new Phrase(model));
                    celda1.BackgroundColor = new BaseColor(255, 51, 51);
                    celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table.AddCell(celda1);

                    PdfPCell celda2 = new PdfPCell(new Phrase(""));
                    celda2.BackgroundColor = new BaseColor(255, 255, 255);
                    celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    celda2.BorderWidthTop = 0;
                    celda2.BorderWidthRight = 0;
                    celda2.Colspan = 5;
                    table.AddCell(celda2);
                    cant = 0;
                }
                else
                {
                    table = new PdfPTable(1);
                    float[] values = new float[1] { 80 };
                    table.SetWidths(values);
                    PdfPCell celda1 = new PdfPCell(new Phrase(""));
                    table.AddCell(celda1);
                }
            }
            return table;
        }
        
    }
}