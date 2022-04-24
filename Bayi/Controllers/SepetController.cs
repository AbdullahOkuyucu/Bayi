using Business.Models;
using Business.Servis.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bayi.Controllers
{
    [Authorize(Roles = "Bayi")]
    public class SepetController : Controller
    {
        private readonly IUrunServis _urunServis;

        public SepetController(IUrunServis urunServis)
        {
            _urunServis = urunServis;
        }
        public IActionResult Ekle(int? urunId)
        {
            if (urunId == null)
            {
                return View("Hata", "Ürün Adı boş olamaz!");
            }
            List<SepetModel> kova = new List<SepetModel>();
            string kovaJson;
            var urun = _urunServis.Query().SingleOrDefault(u => u.Id == urunId.Value);
            string kullaniciId = User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;
            if (HttpContext.Session.GetString("Kova") != null)
            {
                kovaJson = HttpContext.Session.GetString("Kova");
                kova = JsonConvert.DeserializeObject<List<SepetModel>>(kovaJson);
            }
            kova.Add(new SepetModel()
            {
                UrunId = urunId.Value,
                UrunAdi = urun.Adi,
                BirimFiyati = urun.BirimFiyati,
                KullaniciId = Convert.ToInt32(kullaniciId)
            });
            kovaJson = JsonConvert.SerializeObject(kova);
            HttpContext.Session.SetString("Kova", kovaJson);
            return RedirectToAction("Index", "Urun");
        }

        public IActionResult Getir()
        {
            List<SepetModel> kova = new List<SepetModel>();
            if (HttpContext.Session.GetString("Kova") != null)
            {
                kova = JsonConvert.DeserializeObject<List<SepetModel>>(HttpContext.Session.GetString("Kova"));
            }
            List<SepetToplamiModel> sepetToplamiModel = (from k in kova
                                                             //group s by s.UrunAdi
                                                         group k by new { k.UrunId, k.KullaniciId, k.UrunAdi }
                                                        into sGroupBy
                                                         select new SepetToplamiModel()
                                                         {
                                                             UrunId = sGroupBy.Key.UrunId,
                                                             KullaniciId = sGroupBy.Key.KullaniciId,
                                                             UrunAdi = sGroupBy.Key.UrunAdi,
                                                             BirimFiyatToplami = sGroupBy.Sum(s => s.BirimFiyati),
                                                             ToplamUrunSayisi = sGroupBy.Count()
                                                         }).ToList();
            return View(sepetToplamiModel);
        }

        public IActionResult Temizle()
        {
            HttpContext.Session.Remove("Kova");
            return RedirectToAction(nameof(Getir));
        }

        public IActionResult Sil(int urunId, int kullaniciId)
        {
            List<SepetModel> kova = null;
            if (HttpContext.Session.GetString("Kova") != null)
            {
                kova = JsonConvert.DeserializeObject<List<SepetModel>>(HttpContext.Session.GetString("Kova"));
            }
            if (kova != null)
            {
                var kovaItem = kova.FirstOrDefault(s => s.UrunId == urunId && s.KullaniciId == kullaniciId);
                kova.Remove(kovaItem);
                var kovaJson = JsonConvert.SerializeObject(kova);
                HttpContext.Session.SetString("Kova", kovaJson);
            }
            return RedirectToAction(nameof(Getir));
        }
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
