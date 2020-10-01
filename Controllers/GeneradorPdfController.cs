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
            //List<ContratistaCLS> elements = null;

            //using (var bd = new CCDevEntities())
            //{
            //    elements = (from c in bd.Contratistas
            //                join m in bd.Municipios
            //                on c.IDMUNICIPIO equals m.IDMUNICIPIO
            //                join ent in bd.Entidads
            //                on m.IDENTIDAD equals ent.IDENTIDAD
            //                join reg in bd.Regions
            //                on m.IDREGION equals reg.IDREGION
            //                where c.IDCONTRATISTA.Equals(contratista)
            //                select new ContratistaCLS
            //                {
            //                    Nombre = c.NOMBRE,
            //                    Rfc = c.RFC,
            //                    Curp = c.CURP,
            //                    Telefono = c.TELEFONO,
            //                    Correo = c.CORREO,
            //                    Calle = c.CALLE,
            //                    NoExterior = c.NOEXTERIOR,
            //                    NoInterior = c.NOINTERIOR,
            //                    Colonia = c.COLONIA,
            //                    Año = (int)c.AÑO,
            //                    Cp = (int)c.CP,
            //                    municipio = m.MUNICIPIO1,
            //                    entidad = ent.ENTIDAD1,
            //                    region = reg.REGION1
            //                }).ToList();
            //}
                return View();
        }

        public JsonResult mostrarContratista(int contratista)
        {
            List<ContratistaResumenCLS> elements= null;
            
            List<ContratistaResumenCLS> elementsRegistro = null;
            List<ContratistaResumenCLS> elementsRepresenta = null;
            List<ContratistaResumenCLS> elementsPoder = null;
            using (var bd = new CCDevEntities())
            {
                int cant = 0;
                cant = bd.DatosMorals.Where(c => c.IDCONTRATISTA==contratista).Count();
                if (cant.Equals(1))
                {
                    elements = (from c in bd.Contratistas
                                join m in bd.Municipios
                                on c.IDMUNICIPIO equals m.IDMUNICIPIO
                                join ent in bd.Entidads
                                on m.IDENTIDAD equals ent.IDENTIDAD
                                join reg in bd.Regions
                                on m.IDREGION equals reg.IDREGION
                                join mo in bd.DatosMorals
                                on c.IDCONTRATISTA equals mo.IDCONTRATISTA
                                into concat
                                from moral in concat.DefaultIfEmpty()
                                join mun in bd.Municipios
                                on moral.IDMUNICIPIO equals mun.IDMUNICIPIO
                                where c.IDCONTRATISTA.Equals(contratista)
                                select new ContratistaResumenCLS
                                {
                                    Nombre = c.NOMBRE,
                                    Rfc = c.RFC,
                                    Curp = c.CURP,
                                    Tel = c.TELEFONO,
                                    Correo = c.CORREO,
                                    Calle = c.CALLE,
                                    NoExt = c.NOEXTERIOR,
                                    NoInt = c.NOINTERIOR,
                                    Colonia = c.COLONIA,
                                    AñoReg = (int)c.AÑO,
                                    Cp = (int)c.CP,
                                    Municipio = m.MUNICIPIO1,
                                    Entidad = ent.ENTIDAD1,
                                    Region = reg.REGION1,
                                    ActaConstitutiva = moral.ACTACONSTITUTIVA,
                                    FechaActa = moral.FECHA,
                                    NotarioNum = moral.NOTARIONUM,
                                    NotarioNombre = moral.NOTARIONOMBRE,
                                    RegPublico = moral.REGPUBLICO,
                                    MunicipioMor = mun.MUNICIPIO1
                                }).ToList();
                }
                else
                {
                    elements = (from c in bd.Contratistas
                                join m in bd.Municipios
                                on c.IDMUNICIPIO equals m.IDMUNICIPIO
                                join ent in bd.Entidads
                                on m.IDENTIDAD equals ent.IDENTIDAD
                                join reg in bd.Regions
                                on m.IDREGION equals reg.IDREGION
                                where c.IDCONTRATISTA.Equals(contratista)
                                select new ContratistaResumenCLS
                                {
                                    Nombre = c.NOMBRE,
                                    Rfc = c.RFC,
                                    Curp = c.CURP,
                                    Tel = c.TELEFONO,
                                    Correo = c.CORREO,
                                    Calle = c.CALLE,
                                    NoExt = c.NOEXTERIOR,
                                    NoInt = c.NOINTERIOR,
                                    Colonia = c.COLONIA,
                                    AñoReg = (int)c.AÑO,
                                    Cp = (int)c.CP,
                                    Municipio = m.MUNICIPIO1,
                                    Entidad = ent.ENTIDAD1,
                                    Region = reg.REGION1
                                }).ToList();
                }
                                  
            
                    elementsRegistro = (from r in bd.RegistroContratistas
                                        join ic in bd.InfoCapitals
                                        on r.IDINFOCAPITAL equals ic.IDINFOCAPITAL
                                        join tc in bd.TipoContratistas
                                        on r.IDTIPO equals tc.IDTIPO
                                        join e in bd.Especialidads
                                        on r.IDESPECIALIDAD equals e.IDESPECIALIDAD
                                        where r.IDCONTRATISTA==contratista
                                        select new ContratistaResumenCLS
                                        {
                                            Folio= r.REGCONTRALORIAFOLIO,
                                            Ingreso=r.REGCONTRALORIAINGREsO,
                                            Expedicion=r.EXPEDICION,
                                            Vigencia =r.VIGENCIA,
                                            Capital=r.CAPITAL,
                                            InfoCapital=ic.INFOCAPITAL1,
                                            FechaInf=r.INF,
                                            Tipo=tc.TIPO,
                                            Especialidad=e.ESPECIALIDAD1,
                                            Activo=r.ACTIVO
                                        }).ToList();

                    elements.AddRange(elementsRegistro);

                elementsRepresenta = (from re in bd.DatosRepresentas
                                      join ti in bd.TblIdentificacions
                                      on re.ACREDITA equals ti.IDIDENTIFICACION
                                      join td in bd.TblIdentificacions
                                      on re.IDIDENTIFICACION equals td.IDIDENTIFICACION
                                      where re.IDCONTRATISTA == contratista
                                      select new ContratistaResumenCLS
                                      {
                                        Representada = re.REPRESENTADA,
                                        Puesto=re.PUESTO,
                                        Acredita=ti.DESCRIPCION,
                                        NAcredita=re.NACREDITA,
                                        Identificacion=td.DESCRIPCION,
                                        NIden=re.NIDEN
                                      }).ToList();
                elements.AddRange(elementsRepresenta);

                elementsPoder = (from dp in bd.DatosPoders
                                 join m in bd.Municipios
                                 on dp.IDMUNICIPIO equals m.IDMUNICIPIO
                                 join dr in bd.DatosRepresentas
                                 on dp.IDREPRESENTA equals dr.IDREPRESENTA
                                 join c in bd.Contratistas
                                 on dr.IDCONTRATISTA equals c.IDCONTRATISTA
                                 where c.IDCONTRATISTA == contratista
                                 select new ContratistaResumenCLS
                                 {
                                     PoderRep=dp.PODEREP,
                                     Fecha=dp.FECHA,
                                     NotNo=dp.NOTARIONO,
                                     NotNombre=dp.NOTARIONOMBRE,
                                     MunPoder=m.MUNICIPIO1
                                 }).ToList();
                elements.AddRange(elementsPoder);

            }
            return Json(elements, JsonRequestBehavior.AllowGet);
        }

        //public FileResult PdfContratista(int contratista)
        //{

        //    int idcont = 0;
        //    idcont = contratista;
        //    Document doc = new Document();
        //    byte[] buffer;

        //    using(MemoryStream ms= new MemoryStream())
        //    {
        //        PdfWriter.GetInstance(doc, ms);
        //        doc.Open();

        //        //imagen
        //        //----------------------------------------------------//
        //        //titulo
        //        Paragraph title = new Paragraph("Contratista");
        //        title.Alignment = Element.ALIGN_CENTER;
        //        doc.Add(title);
        //        //----------------------------------------------------//
        //        Paragraph espacio = new Paragraph(" ");
        //        doc.Add(espacio);

        //        ///----------------------------------------------------//
        //        ///
        //        ///General
        //        ///
        //        ///----------------------------------------------------//
        //        ///

        //        string[] modelos = { "Contratista", "Moral","Registro","Poder","Representa" };
        //        foreach(string el in modelos)
        //        {
        //            //si existe el id en el modelo
        //            if (consultaId(idcont,el)==1)
        //            {
        //                PdfPTable tabla = encabezado(el);
        //                doc.Add(tabla);

        //                if (fillitems(idcont, el) != null)
        //                {
        //                    PdfPTable table1 = fillitems(idcont, el);
        //                    doc.Add(table1);
        //                    Paragraph espacio2 = new Paragraph(" ");
        //                    doc.Add(espacio2);
        //                }

        //            }
        //            //llena la tabla con el modelo y el id

        //        }           
        //        doc.Close();
        //        buffer = ms.ToArray();
        //    }

        //    return File( buffer,"application/pdf");
        //}
        //public int consultaId(int bus, string model)
        //{
        //    int consulta = 0;
        //    int cantR = 0;
        //    int idpod = 0;

        //    using (var bd = new CCDevEntities())
        //    {
        //        switch (model)
        //        {
        //            case "Contratista":
        //                consulta = bd.Contratistas.Where(c => c.IDCONTRATISTA.Equals(bus)).Count();
        //                break;

        //            case "Moral":
        //                consulta = bd.DatosMorals.Where(m => m.IDCONTRATISTA == bus).Count();
        //                break;

        //            case "Registro":
        //                consulta = bd.RegistroContratistas.Where(rc => rc.IDCONTRATISTA == bus).Count();
        //                break;

        //            case "Representa":
        //                consulta = bd.DatosRepresentas.Where(r => r.IDCONTRATISTA == bus).Count();
        //                break;

        //            case "Poder":
        //                cantR = bd.DatosRepresentas.Where(r => r.IDCONTRATISTA == bus).Count();

        //                idpod = Convert.ToInt32(cantR);
        //                consulta = bd.DatosPoders.Where(p => p.IDREPRESENTA == idpod).Count();
        //                break;
        //        }


        //    }

        //    return consulta;
        //}

        //public PdfPTable encabezado(string model)
        //{


        //    PdfPTable table = null;
        //    using (var bd= new CCDevEntities())
        //    {
        //             table = new PdfPTable(2);
        //            float[] values = new float[2] { 30, 80 };
        //            table.SetWidths(values);

        //            PdfPCell celda1 = new PdfPCell(new Phrase(model));
        //            celda1.BackgroundColor = new BaseColor(255, 51, 51);
        //            celda1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //            table.AddCell(celda1);

        //            PdfPCell celda2 = new PdfPCell(new Phrase(""));
        //            celda2.BackgroundColor = new BaseColor(255, 255, 255);
        //            celda2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        //            celda2.BorderWidthTop = 0;
        //            celda2.BorderWidthRight = 0;
        //            celda2.Colspan = 5;
        //            table.AddCell(celda2);
        //    }
        //    return table;
        //}

        //public PdfPTable fillitems(int idcontratista, string model)
        //{
        //    PdfPTable table = null;
        //    List<ContratistaCLS> elements = null;
        //    List<MoralCLS> elementsMoral = null;
        //    List<RegistroContratistaCLS> elementsRegistro = null;
        //    int itemList = 0;
        //    string nombreAtt = "";
        //    string nombre = "";
        //    string valor = null;

        //    using (var bd = new CCDevEntities())
        //    {
        //        switch (model)
        //        {
        //            case "Contratista":

        //                elements = (from c in bd.Contratistas
        //                            join m in bd.Municipios
        //                            on c.IDMUNICIPIO equals m.IDMUNICIPIO
        //                            join ent in bd.Entidads
        //                            on m.IDENTIDAD equals ent.IDENTIDAD
        //                            join reg in bd.Regions
        //                            on m.IDREGION equals reg.IDREGION
        //                            where c.IDCONTRATISTA.Equals(idcontratista)
        //                            select new ContratistaCLS
        //                            {
        //                                Nombre = c.NOMBRE,
        //                                Rfc = c.RFC,
        //                                Curp = c.CURP,
        //                                Telefono = c.TELEFONO,
        //                                Correo = c.CORREO,
        //                                Calle = c.CALLE,
        //                                NoExterior = c.NOEXTERIOR,
        //                                NoInterior = c.NOINTERIOR,
        //                                Colonia = c.COLONIA,
        //                                Año = (int)c.AÑO,
        //                                Cp = (int)c.CP,
        //                                municipio = m.MUNICIPIO1,
        //                                entidad = ent.ENTIDAD1,
        //                                region = reg.REGION1
        //                            }).ToList();

        //                PropertyInfo[] lst = typeof(ContratistaCLS).GetProperties();
        //                int tam = lst.Length;
        //                table = new PdfPTable(6);

        //                foreach (PropertyInfo propertyInfo in lst)
        //                {
        //                    nombreAtt = propertyInfo.Name;
        //                    //validacion de campos que no iran en el reporte
        //                    if (nombreAtt != "IdContratista" && nombreAtt != "IdMunicipio")
        //                    {
        //                        ///si no tiene datos le ponemos vacio
        //                        if (propertyInfo.GetValue(elements[0]) == null)
        //                        {
        //                            nombre = nombreAtt;
        //                            valor = "";
        //                            itemList++;

        //                        }
        //                        else
        //                        {
        //                            nombre = nombreAtt;
        //                            valor = propertyInfo.GetValue(elements[0]).ToString();
        //                            itemList++;
        //                            //encabezados
        //                            if (nombreAtt == "Nombre")
        //                            {
        //                                PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 153, 153),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_LEFT,



        //                                };
        //                                table.AddCell(head);
        //                                //datos
        //                                PdfPCell text = new PdfPCell(new Phrase(valor))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 255, 255),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_CENTER,
        //                                    Colspan = 3
        //                                };
        //                                table.AddCell(text);
        //                            }
        //                            else if (nombreAtt == "Calle" || nombreAtt == "Correo")
        //                            {
        //                                PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 153, 153),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_LEFT
        //                                };
        //                                table.AddCell(head);
        //                                //datos
        //                                PdfPCell text = new PdfPCell(new Phrase(valor))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 255, 255),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_CENTER,
        //                                    Colspan = 3
        //                                };
        //                                table.AddCell(text);
        //                            }
        //                            else
        //                            {
        //                                PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 153, 153),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_LEFT
        //                                };
        //                                table.AddCell(head);
        //                                //datos
        //                                PdfPCell text = new PdfPCell(new Phrase(valor))
        //                                {
        //                                    BackgroundColor = new BaseColor(255, 255, 255),
        //                                    HorizontalAlignment = PdfPCell.ALIGN_CENTER
        //                                };
        //                                table.AddCell(text);
        //                            }
        //                        }
        //                    }
        //                }
        //                break;
        //            case "Moral":
        //                elementsMoral = (from mo in bd.DatosMorals
        //                            join mu in bd.Municipios
        //                            on mo.IDMUNICIPIO equals mu.IDMUNICIPIO
        //                            where mo.IDCONTRATISTA==idcontratista
        //                            select new MoralCLS
        //                            {
        //                                ActaConstitutiva = mo.ACTACONSTITUTIVA,
        //                                FechaActa = mo.FECHAACTA,
        //                                NotarioNum = mo.NOTARIONUM,
        //                                NotarioNombre = mo.NOTARIONOMBRE,
        //                                Municipio = mu.MUNICIPIO1
        //                            }).ToList();
        //                PropertyInfo[] lstMoral = typeof(MoralCLS).GetProperties();
        //                int tamMo = lstMoral.Length;
        //                table = new PdfPTable(6);
        //                foreach (PropertyInfo propertyInfo in lstMoral)
        //                {
        //                    nombreAtt = propertyInfo.Name;
        //                    if (propertyInfo.GetValue(elementsMoral[0]) == null)
        //                    {
        //                        nombre = nombreAtt;
        //                        valor = "";
        //                        itemList++;

        //                    }
        //                    else
        //                    {
        //                        if (nombreAtt != "IdContratista" &&  nombreAtt != "IdMunicipio" && nombreAtt != "RegPublico" )
        //                        {
        //                            nombre = nombreAtt;
        //                            valor = propertyInfo.GetValue(elementsMoral[0]).ToString();
        //                            itemList++;
        //                            PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                            {
        //                                BackgroundColor = new BaseColor(255, 153, 153),
        //                                HorizontalAlignment = PdfPCell.ALIGN_LEFT
        //                            };
        //                            table.AddCell(head);
        //                            //datos
        //                            PdfPCell text = new PdfPCell(new Phrase(valor))
        //                            {
        //                                BackgroundColor = new BaseColor(255, 255, 255),
        //                                HorizontalAlignment = PdfPCell.ALIGN_CENTER
        //                            };
        //                            table.AddCell(text);
        //                        }
        //                        else
        //                        {
        //                            PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                            {
        //                                BackgroundColor = new BaseColor(255, 153, 153),
        //                                HorizontalAlignment = PdfPCell.ALIGN_LEFT
        //                            };
        //                            table.AddCell(head);
        //                            //datos
        //                            PdfPCell text = new PdfPCell(new Phrase(valor))
        //                            {
        //                                BackgroundColor = new BaseColor(255, 255, 255),
        //                                HorizontalAlignment = PdfPCell.ALIGN_CENTER
        //                            };
        //                            table.AddCell(text);
        //                        }
        //                    }
        //                }
        //                    break;
        //            case "Registro":
        //                elementsRegistro = (from r in bd.RegistroContratistas
        //                                    join c1 in bd.Contratistas
        //                                    on r.IDCONTRATISTA equals c1.IDCONTRATISTA
        //                                    join ic in bd.InfoCapitals
        //                                    on r.IDINFOCAPITAL equals ic.IDINFOCAPITAL
        //                                    join tc in bd.TipoContratistas
        //                                    on r.IDTIPO equals tc.IDTIPO
        //                                    join e in bd.Especialidads
        //                                    on r.IDESPECIALIDAD equals e.IDESPECIALIDAD
        //                                    where r.IDCONTRATISTA == idcontratista
        //                                    select new RegistroContratistaCLS
        //                                    {
        //                                        RegContraloriaFolio=r.REGCONTRALORIAFOLIO,
        //                                        RegContraloriaIngreso=r.REGCONTRALORIAINGREGSO,
        //                                        FechaExpedicion = (DateTime)r.FECHAEXPEDICION,
        //                                        FechaVigencia =(DateTime)r.FECHAVIGENCIA,
        //                                        Capital = (decimal)r.CAPITAL,
        //                                        InfoCapital= ic.INFOCAPITAL1,
        //                                        FechaInf=(DateTime)r.FECHAINF,
        //                                        Tipo=tc.TIPO,
        //                                        Especialidad=e.ESPECIALIDAD1,
        //                                        Activo= (Boolean)r.ACTIVO

        //                                    }).ToList();
        //                PropertyInfo[] lstRegistro = typeof(RegistroContratistaCLS).GetProperties();
        //                int tamReg = lstRegistro.Length;
        //                table = new PdfPTable(6);
        //                foreach(PropertyInfo propertyInfo in lstRegistro)
        //                {
        //                    nombreAtt = propertyInfo.Name;
        //                    if (propertyInfo.GetValue(elementsRegistro[0]) == null)
        //                    {
        //                        nombre = nombreAtt;
        //                        valor = "";
        //                        itemList++;
        //                    }
        //                    else
        //                    {
        //                        nombre = nombreAtt;
        //                        valor = propertyInfo.GetValue(elementsRegistro[0]).ToString();
        //                        itemList++;


        //                        PdfPCell head = new PdfPCell(new Phrase(nombre.ToUpper() + ":"))
        //                        {
        //                            BackgroundColor = new BaseColor(255, 153, 153),
        //                            HorizontalAlignment = PdfPCell.ALIGN_LEFT
        //                        };
        //                        table.AddCell(head);
        //                        //datos
        //                        PdfPCell text = new PdfPCell(new Phrase(valor))
        //                        {
        //                            BackgroundColor = new BaseColor(255, 255, 255),
        //                            HorizontalAlignment = PdfPCell.ALIGN_CENTER
        //                        };
        //                        table.AddCell(text);
        //                    }
        //                }
        //                break;
        //            case "Representa":
        //                break;

        //            case "Poder":
        //                break;
        //        }
        //    }
        //    return table;
        //}

    }
}