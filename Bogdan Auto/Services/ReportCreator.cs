using Syncfusion.Pdf;
using Bogdan_Auto.Models;
using System.IO;
using Syncfusion.Pdf.Graphics;

namespace Bogdan_Auto.Services
{
    public class InvoiceCreator
    {
        public static void CreateInvoice(Order order)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.Pages.Add();
            PdfGraphics graphics = page.Graphics;
        }
    }
}
