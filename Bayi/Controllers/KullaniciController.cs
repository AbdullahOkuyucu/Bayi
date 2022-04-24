using AppCore.Business.Results;
using Business.Servis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bayi.Controllers
{
    [Authorize(Roles = "Üretici")]
    public class KullaniciController : Controller
    {
        private readonly IHesapDetayServis _hesapServis;

        public KullaniciController(IHesapDetayServis hesapDetayServis)
        {
            _hesapServis = hesapDetayServis;
        }

        public IActionResult Index()
        {
            var result = _hesapServis.KullanicilariListele();
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            if (result.Status == ResultStatus.Error)
                ViewBag.Message = result.Message;
            return View(result.Data);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Bulunamadı");
            }

            var result = _hesapServis.KullaniciListele(id.Value);
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            if (result.Status == ResultStatus.Error)
                ViewData["Message"] = result.Message;
            return View(result.Data);
        }
    }
}

