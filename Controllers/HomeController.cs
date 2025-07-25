using HTML_to_PDF_Azure_app_service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using Syncfusion.Drawing;

namespace HTML_to_PDF_Azure_app_service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult ExportToPDF()
        {
            HtmlToPdfConverter htmlConverter = null;
            PdfDocument document = null;
            MemoryStream outputStream = null;

            try
            {
                // Create memory stream for the output PDF
                outputStream = new MemoryStream();

                // Initialize HTML to PDF converter
                htmlConverter = new HtmlToPdfConverter(HtmlRenderingEngine.Cef);
                CefConverterSettings cefConverterSettings = new CefConverterSettings();
                cefConverterSettings.ViewPortSize = new Size(1280, 0);
                htmlConverter.ConverterSettings = cefConverterSettings;

                if (true)
                {
                    // Convert URL to PDF document
                    document = htmlConverter.Convert("https://www.google.com/");
                }
                else
                {
                    // Convert HTML string to PDF document
                    document = htmlConverter.Convert("htmlContent", string.Empty);
                }

                document.Save(outputStream);

                // Reset the position of output stream to beginning
                outputStream.Position = 0;

            }
            catch(Exception ex)
            {
                return BadRequest($"Error converting HTML to PDF: {ex.Message.ToString()}");
            }
                return File(outputStream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Pdf, "HTML-to-PDF.pdf");
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
