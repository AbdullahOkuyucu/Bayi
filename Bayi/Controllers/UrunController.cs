#nullable disable
using Business.Models;
using Business.Servis;
using Business.Servis.Base;
using Data.Entity.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bayi.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        private readonly BayiContext _context;
        private readonly IUrunServis _urunServis;
        private readonly IKategoriServis _kategoriServis;

        public UrunController(IUrunServis urunServis, IKategoriServis kategoriServis)
        {
            _urunServis = urunServis;
            _kategoriServis = kategoriServis;
        }
        public IActionResult Index()
        {
            var model = _urunServis.Query().ToList();
            return View(model);
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var urun = _urunServis.Query().SingleOrDefault(u => u.Id == id.Value);
            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            return View(urun);
        }
        [Authorize(Roles = "Üretici")]
        public IActionResult Create()
        {
            var kategoriler = _kategoriServis.Query().ToList();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Adi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Üretici")]
        public IActionResult Create(UrunModel urun)
        {
            if (ModelState.IsValid)
            {
                urun.CreatedBy = User.Identity.Name;
                var result = _urunServis.Add(urun);
                ModelState.AddModelError("", result.Message);
            }
            var kategoriler = _kategoriServis.Query().ToList();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        [Authorize(Roles = "Üretici")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }
            var urun = _urunServis.Query().SingleOrDefault(u => u.Id == id.Value);
            if (urun == null)
            {
                return View("Hata", "Ürün bulunamadı!");
            }
            var kategoriler = _kategoriServis.Query().ToList();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Üretici")]
        public IActionResult Edit(UrunModel urun)
        {
            if (ModelState.IsValid)
            {
                urun.UpdatedBy = User.Identity.Name;
                var result = _urunServis.Update(urun);
                ModelState.AddModelError("", result.Message);
            }
            var kategoriler = _kategoriServis.Query().ToList();
            ViewBag.Kategoriler = new SelectList(kategoriler, "Id", "Adi", urun.KategoriId);
            return View(urun);
        }
        public IActionResult Delete(int? id)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Üretici")))
                return RedirectToAction("YetkisizIslem", "Hesap");

            if (id == null)
            {
                return View("Hata", "Id gereklidir!");
            }

            var existingProduct = _urunServis.Query().SingleOrDefault(u => u.Id == id.Value);
            var result = _urunServis.Delete(id.Value);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
    }
}
