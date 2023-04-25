using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication6.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public IActionResult Index(Ogrenci ogrenci, IFormFile Resim)
        {
            if (Resim == null || Resim.Length == 0)
                return Content("Resim yüklenemedi.");
            var yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Resim.FileName);
            Resim.CopyTo(new FileStream(yol, FileMode.Create));
            return Content("Dosya yükleme tamamlandı.");
        }
        [HttpPost]
        public IActionResult Index(Ogrenci ogrenci)
        {
            if (ogrenci.Resim == null || ogrenci.Resim.Length == 0)
                return Content("Dosya yüklenemedi.");
            var yol = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ogrenci.Resim.FileName);
            ogrenci.Resim.CopyTo(new FileStream(yol, FileMode.Create));
            return Content("Dosya yükleme tamamlandı.");
        }
        public IActionResult Ekle(Ogrenci ogrenci)
        {
            if (ogrenci.OkulNo < 0)
            {
                return Content("Geçerli bir okul numarası giriniz.");
            }

            if (string.IsNullOrEmpty(ogrenci.OgrenciAdi))
            {
                return Content("Öğrenci adı boş bırakılamaz.");
            }
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
