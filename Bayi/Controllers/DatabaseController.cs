using Data.Entity.Context;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Bayi.Controllers
{
    public class DatabaseController : Controller
    {
        public IActionResult Seed()
        {
            using (BayiContext db = new BayiContext())
            {
                var urunEntiti = db.Urunler.ToList();
                db.Urunler.RemoveRange(urunEntiti);
                var kategoriEntiti = db.Kategoriler.ToList();
                db.Kategoriler.RemoveRange(kategoriEntiti);
                var hesaplarEntiti = db.Hesaplar.ToList();
                db.Hesaplar.RemoveRange(hesaplarEntiti);
                var hesapDetayEntiti = db.HesapDetaylari.ToList();
                db.HesapDetaylari.RemoveRange(hesapDetayEntiti);
                var rolEntiti = db.Roller.ToList();
                db.Roller.RemoveRange(rolEntiti);

                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Konserve",
                    Urunler = new List<Urun>
                    {
                        new Urun()
                        {
                            Adi = "Bezelye",
                            BirimFiyati = 10,
                            StokMiktari = 100,
                            Aciklamasi = "İri taneli",
                            SonKullanmaTarihi = new DateTime(2023, 3, 13)
                        },
                        new Urun()
                        {
                            Adi = "Patlıcan",
                            BirimFiyati = 15,
                            StokMiktari = 150,
                            Aciklamasi = "Közlenmiş",
                            SonKullanmaTarihi = new DateTime(2023, 5, 15)
                        },
                        new Urun()
                        {
                            Adi = "Salatalık Turşusu",
                            BirimFiyati = 20,
                            StokMiktari = 200,
                            Aciklamasi = "Dilmli",
                            SonKullanmaTarihi = new DateTime(2023, 7, 7)
                        }
                    }
                });
                db.Kategoriler.Add(new Kategori()
                {
                    Adi = "Kahve",
                    Aciklamasi = "Toz ve Çekirdek Kahveler",
                    Urunler = new List<Urun>()
                    {
                         new Urun()
                        {
                            Adi = "Jacobs",
                            BirimFiyati = 80,
                            StokMiktari = 8
                        },
                        new Urun()
                        {
                            Adi = "Nescafe",
                            BirimFiyati = 70,
                            StokMiktari = 7
                        },
                        new Urun()
                        {
                            Adi = "Tchibo",
                            BirimFiyati = 90,
                            StokMiktari = 9
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Üretici",
                    Hesap = new List<Hesap>()
                    {
                        new Hesap()
                        {
                            KullaniciAdi = "admin",
                            Sifre = "admin",
                            Aktif = true,
                            HesapDetayi =  new HesapDetayi()
                            {
                                EPosta = "admin@bayi.com.tr",
                                Adres = "İzmir, Bornova"
                            }
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Bayi",
                    Hesap = new List<Hesap>()
                    {
                        new Hesap()
                        {
                            KullaniciAdi = "bayi",
                            Sifre = "bayi",
                            Aktif = true,
                            HesapDetayi =  new HesapDetayi()
                            {
                                EPosta = "bayi@bayi.com.tr",
                                Adres = "Mersin, Mezitli"
                            }
                        }
                    }
                });
                db.Roller.Add(new Rol()
                {
                    Adi = "Kullanici",
                    Hesap = new List<Hesap>()
                    {
                        new Hesap()
                        {
                            KullaniciAdi = "Kullanici",
                            Sifre = "Kullanici",
                            Aktif = true,
                            HesapDetayi =  new HesapDetayi()
                            {
                                EPosta = "Kullanici@bayi.com.tr",
                                Adres = "Bodrum, Gümbet"
                            }
                        }
                    }
                });
                //db.ExecuteStoreCommand("DBCC CHECKIDENT('Roller',RESEED,1);");
                
                db.SaveChanges();
            }
            return Content("<label style=\"color:red;\"><b>İlk veriler oluşturuldu.</b></label>", "text/html", Encoding.UTF8);
        }      
    }
}
