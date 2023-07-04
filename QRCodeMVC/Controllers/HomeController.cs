using Microsoft.AspNetCore.Mvc;
using QRCodeMVC.Models;
using QRCoder;
using static QRCoder.PayloadGenerator;

namespace QRCodeMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new QRCodeModel());
        }

        [HttpPost]
        public IActionResult Index(QRCodeModel model)
        {
            Payload payload = new Url(model.WebSiteURLText);
            QRCodeGenerator codeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = codeGenerator.CreateQrCode(payload);
            QRCoder.PngByteQRCode pngByteQRCode=new PngByteQRCode(qRCodeData);

            var qrASByte = pngByteQRCode.GetGraphic(20);

            string base64String=Convert.ToBase64String(qrASByte);
            model.QRImageURL = "data:image/png;base64," + base64String;
            return View("Index", model);
        }
    }
}