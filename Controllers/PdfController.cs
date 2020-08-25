using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace ConcursosContratos.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdg
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pdf()
        {
            MemoryStream ms = new MemoryStream();

            PdfWriter pw = new PdfWriter(ms);

            PdfDocument pdfDocument = new PdfDocument(pw);
            Document doc = new Document(pdfDocument,PageSize.LETTER);
            //Agregar Margen
            doc.SetMargins(75,35,70,35);

            string nameFont = Server.MapPath();
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.HELVETICA);
            doc.Add(new Paragraph("hola itext")
                .SetFontColor(ColorConstants.BLUE)
                .SetFont(font).SetFontSize(24));
            doc.Close();

            byte[] byteStream = ms.ToArray();
            ms = new MemoryStream();
            ms.Write(byteStream, 0, byteStream.Length);
            ms.Position = 0;
            return new FileStreamResult(ms,"application/pdf");
        }
    }
}