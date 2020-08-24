using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using itext.io;

namespace ConcursosContratos.Controllers
{
    public class PdfController : Controller
    {
        // GET: Pdf
        public ActionResult ContratistaPdf()
        {
            PdfWriter ow = new PdfWriter("");
            FileStream fs = new FileStream("c://pdf/reporte.pdf",FileMode.Create);
            Document document= new Document()
            return View();
        }
    }
}