using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using ConcursosContratos.Models;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace ConcursosContratos.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdg
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PdfContratista()
        {
            int contratistaConsulta = 0;

            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);

            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument, PageSize.LETTER);
            //Agregar Margen
            doc.SetMargins(75, 35, 70, 35);

            //Tabla
            //Paragraph title = new Paragraph("Lista Contratista");
            //title.SetTextAlignment(TextAlignment.CENTER);
            //doc.Add(title);
            ///Agregar imagen
            string pathLogo = Server.MapPath("~/Content/img/logoSidum.png");
            Image image = new Image(ImageDataFactory.Create(pathLogo));
            //Encabezado
            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler1(image));
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, new FooterEventHandler1());
            //pie de pagina
            ///


            Table table = new Table(1).UseAllAvailableWidth();
            Cell cell = new Cell().Add(new Paragraph("Contratistas").SetFontSize(14))
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBorder(Border.NO_BORDER);
            table.AddCell(cell);
        
            doc.Add(table);



            string nameFont = Server.MapPath("~/fonts/MyriadPro-Semibold.otf");
            PdfFont font = PdfFontFactory.CreateFont(nameFont);

            string nameFont1 = Server.MapPath("~/fonts/interstate-black-cond.otf");
            PdfFont font1 = PdfFontFactory.CreateFont(nameFont1);

            string nFont = Server.MapPath("~/fonts/interstate-light-italic.otf");
            PdfFont font2 = PdfFontFactory.CreateFont(nFont);
            //estilo de fuente

            //ancho de columna
            
            Style stylescel = new Style()
                 .SetFontSize(16)
                .SetFont(font)
                // .SetFontColor(ColorConstants.BLUE)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBackgroundColor(ColorConstants.);

            //encabezado del primer grupo de datos
            Table _tableTitulo = new Table(2).UseAllAvailableWidth();
            Cell _cellTitulo = new Cell(1,1).Add(new Paragraph("Datos Generales"));
            _tableTitulo.AddHeaderCell(_cellTitulo.AddStyle(stylescel));
            doc.Add(_tableTitulo);

            Table _table = new Table(6).UseAllAvailableWidth();
            ContratistaCLS contratistaCLS = new ContratistaCLS();
            using (var bd = new CCDevEntities())
            {
                Contratista contratista = bd.Contratistas.Where(c => c.IDCONTRATISTA == 11).First();

                contratistaCLS.Rfc = contratista.RFC;
                contratistaCLS.Nombre = contratista.NOMBRE;
                contratistaCLS.Telefono = contratista.TELEFONO;
                contratistaCLS.Calle = contratista.CALLE;
                contratistaCLS.NoExterior = contratista.NOEXTERIOR;
                contratistaCLS.NoInterior = contratista.NOINTERIOR;
                contratistaCLS.Colonia = contratista.COLONIA;
                contratistaCLS.Cp = (int)contratista.CP;
                contratistaCLS.Curp = contratista.CURP;
                contratistaCLS.IdMunicipio = (int)contratista.IDMUNICIPIO;
                contratistaCLS.Correo = contratista.CORREO;
                contratistaCLS.Año = (int)contratista.AÑO;
            }
            Cell _cell = new Cell(1,1).Add(new Paragraph("NOMBRE")
                .SetFont(font1)
                .SetWidth(45f)
                .SetFontSize(10));           
            _table.AddCell(_cell);


            Cell  _cell1= new Cell(1,6).Add(new Paragraph(contratistaCLS.Nombre)
                .SetFont(font2)
                .SetWidth(130f)
                .SetFontSize(9));
            _table.AddCell(_cell1);
            Cell _cell8 = new Cell(2,1).Add(new Paragraph("DIRECCION")
                
             .SetFont(font1)
              .SetWidth(45f)
             .SetFontSize(10));
            _table.AddCell(_cell8);

            Cell _cell9 = new Cell(2,6).Add(new Paragraph(contratistaCLS.Calle + " #" + contratistaCLS.NoExterior + " " + contratistaCLS.Colonia).SetFont(font2)
               .SetFont(font2)
                .SetWidth(100f)
                .SetFontSize(9));
            _table.AddCell(_cell9);

            Cell _cell2 = new Cell().Add(new Paragraph("RFC")
                .SetFont(font1)
                 .SetWidth(25f)
                .SetFontSize(10));
            _table.AddCell(_cell2);

          Cell  _cell3 =  new Cell().Add(new Paragraph(contratistaCLS.Rfc)
            .SetFont(font2)
             .SetWidth(35f)
                .SetFontSize(9));
            _table.AddCell(_cell3);

            Cell _cell4 = new Cell().Add(new Paragraph("CURP")
                .SetFont(font1)
                .SetWidth(25f)
                .SetFontSize(10));
            _table.AddCell(_cell4);


           Cell _cell5 = new Cell().Add(new Paragraph(contratistaCLS.Curp)
               .SetFont(font2)
               .SetWidth(35f)
                .SetFontSize(9));
            _table.AddCell(_cell5);

            Cell _cell6 = new Cell().Add(new Paragraph("TEL.")
                .SetFont(font1)
                .SetWidth(25f)
                .SetFontSize(10));
            _table.AddCell(_cell6);
            
          Cell  _cell7 = new Cell().Add(new Paragraph(contratistaCLS.Telefono)
              .SetFont(font2)
              .SetWidth(35f)
                .SetFontSize(9));
            _table.AddCell(_cell7);

         
            //_cell = new Cell(2, 5).Add(new Paragraph("RFC"));
            //_table.AddHeaderCell(_cell.AddStyle(stylescel));
            //_cell = new Cell().Add(new Paragraph("CURP"));
            //_table.AddHeaderCell(_cell.AddStyle(stylescel));
            //_cell = new Cell().Add(new Paragraph("TELEFONO"));
            //_table.AddHeaderCell(_cell.AddStyle(stylescel));
            //_cell = new Cell().Add(new Paragraph("E-MAIL"));
            //_table.AddHeaderCell(_cell.AddStyle(stylescel));


            //List<Municipio> listaMun;
            //using (var bd = new CCDevEntities())
            //{
            //    listaMun =bd.Municipios.Where(m=>m.IDMUNICIPIO>0).ToList();
            //    int x = 0;
            //    foreach(var item in listaMun)
            //    {
            //        x++;
            //        _cell = new Cell(2,4).Add(new Paragraph(x.ToString()));
            //        _table.AddCell(_cell.SetBackgroundColor(ColorConstants.GREEN));
            //        _cell = new Cell(2,6).Add(new Paragraph(item.IDENTIDAD.ToString()));
            //        if (item.IDENTIDAD<2)
            //        {
            //            _table.AddCell(_cell.SetBackgroundColor(ColorConstants.ORANGE).SetFontColor(ColorConstants.RED));
            //        }
            //        else
            //        {
            //            _table.AddCell(_cell.SetBackgroundColor(ColorConstants.ORANGE));
            //        }
            //        _cell = new Cell().Add(new Paragraph(item.IDMUNICIPIO.ToString()));
            //        _table.AddCell(_cell.SetBackgroundColor(ColorConstants.YELLOW));
            /// validacion para cambio de color de fondo
            /// if(item.unidad<10)
            /// {
            ///  _cell = new Cell().Add(new Paragraph(item.MUNICIPIO1));
            ///  _table.AddCell(_cell.SetBackgroundColor(ColorConstants.RED));
            ///  }
            /// else
            /// {
            /// _cell = new Cell().Add(new Paragraph(item.MUNICIPIO1));
            ///  _table.AddCell(_cell);
            /// 

            //_cell = new Cell().Add(new Paragraph(item.MUNICIPIO1));

            //    }
            //}

            doc.Add(_table);

            doc.Close();
            
            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;
            return new FileStreamResult(ms,"application/pdf");
        }
    }
    public class HeaderEventHandler1 : IEventHandler
    {
        Image Imagen;
        public HeaderEventHandler1(Image image)
        {
            Imagen = image;
        }
        public void HandleEvent(Event @event)
        {
            //documento a evento para tener acceso para añadir contenido
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas1 = new PdfCanvas(page.NewContentStreamAfter(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 75, page.GetPageSize().GetWidth() - 72, 60);
            new Canvas(canvas1, pdfDoc,rootArea)
                .Add(getTable(docEvent));

            //Rectangle rootArea = new Rectangle(35, page.GetPageSize().GetTop() - 70, page.GetPageSize().GetRight() - 70, 50);

            //Canvas canvas = new Canvas(page, rootArea);
            //canvas
            //    .Add(getTable(docEvent))
            //    .ShowTextAligned("Este es el encabezado", 10, 0, TextAlignment.CENTER)
            //    .ShowTextAligned("Este es el pie", 10, 10, TextAlignment.CENTER)
            //    .ShowTextAligned("texto agregado", 612, 0, TextAlignment.RIGHT)
            //    .Close();
        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 20f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);

            Style styleText = new Style()
                .SetTextAlignment(TextAlignment.RIGHT).SetFontSize(10f);

            Cell cell = new Cell().Add(Imagen.ScaleToFit(420f,150f));

            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.LEFT));

            PdfFont bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            cell = new Cell()
                .Add(new Paragraph("Reporte diario\n").SetFont(bold))
             .Add(new Paragraph("Departamento de RS\n").SetFont(bold))
              .Add(new Paragraph("Fecha emision: " + DateTime.Now.ToShortDateString()))
              .AddStyle(styleText).AddStyle(styleCell);
              
            tableEvent.AddCell(cell);

            return tableEvent;
        }
    }
    public class FooterEventHandler1:IEventHandler
    {
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDoc = docEvent.GetDocument();
            PdfPage page = docEvent.GetPage();

            PdfCanvas canvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdfDoc);
            Rectangle rootArea = new Rectangle(36,20, page.GetPageSize().GetWidth() - 70, 50);
            new Canvas(canvas, pdfDoc, rootArea)
                .Add(getTable(docEvent));
        }
        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidth = { 92f, 8f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidth)).UseAllAvailableWidth();

            //    PdfPage page = docEvent.GetPage();
            //      int pageNum = docEvent.GetDocument().GetPageNumber(page);
            
            int pageNum = docEvent.GetDocument().GetPageNumber(docEvent.GetPage());
            Style styleCell = new Style()
                .SetPadding(5)
                .SetBorder(Border.NO_BORDER)
                .SetBorderTop(new SolidBorder(ColorConstants.BLACK, 2));

            Cell cell = new Cell().Add(new Paragraph(DateTime.Now.ToLongDateString()));
            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontColor(ColorConstants.LIGHT_GRAY));

            cell = new Cell().Add(new Paragraph(pageNum.ToString()));
            cell.AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.BLACK)
                .SetFontColor(ColorConstants.WHITE)
                .SetTextAlignment(TextAlignment.CENTER);
            tableEvent.AddCell(cell);
            return tableEvent;
        }
        
    }
}