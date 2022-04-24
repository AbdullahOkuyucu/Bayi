using AppCore.Business.Results;
using Business.Models;
using Business.Servis;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Bayi.Controllers
{
    public class HesapController : Controller
    {
        private readonly IHesapServis _hesapServis;

        public HesapController(IHesapServis hesapServis)
        {
            _hesapServis = hesapServis;
        }
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Kayit(HesapKayitModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _hesapServis.Kayit(model);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View();
                }
                
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public IActionResult GirisIslemi()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> GirisIslemi(HesapGirisModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _hesapServis.GirisIslemi(model);
                if (!result.IsSuccessful)
                {
                    ModelState.AddModelError("", result.Message);
                    return View();
                }
                //Başarılı
                var hesap = result.Data;
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, hesap.KullaniciAdi),
                    new Claim(ClaimTypes.Role, hesap.RolModel.Adi),
                    new Claim(ClaimTypes.Sid, hesap.Id.ToString())
                };
                var identitie = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identitie);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public async Task<IActionResult> CikisIslem()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult YetkisizIslem()
        {
            return View();
        }
    }
}
