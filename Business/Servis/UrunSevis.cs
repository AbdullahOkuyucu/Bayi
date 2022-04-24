using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Results;
using Business.Models;
using Business.Models.Filtre;
using Business.Models.Rapor;
using Business.Servis.Base;
using Data.Entity.Repositories.Base;
using Entities.Entities;
using System.Globalization;

namespace Business.Servis
{
    public class UrunServis : IUrunServis
    {
        private readonly UrunRepoBase _urunRepo;
        private readonly KategoriRepoBase _kategoriRepo;

        public UrunServis(UrunRepoBase urunRepo, KategoriRepoBase kategoriRepo)
        {
            _urunRepo = urunRepo;
            _kategoriRepo = kategoriRepo;
        }
        public Result Add(UrunModel model)
        {
            if (_urunRepo.EntityQuery().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim()))
            {
                return new ErrorResult("Aynı ürün adına sahip kayıt bulunmaktadır!");
            }
            DateTime? sonKullanmaTarihi = string.IsNullOrWhiteSpace(model.SonKullanmaTarihiModel) ? null : DateTime.Parse(model.SonKullanmaTarihiModel, new CultureInfo("tr-TR"));
            if (sonKullanmaTarihi != null && sonKullanmaTarihi.Value <= DateTime.Today)
            {
                return new ErrorResult("Son kullanma tarihi yarın veya daha sonraki bir tarih olmalıdır!");
            }
            double birimFiyati = Convert.ToDouble(model.BirimFiyatiModel.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
            if (!(birimFiyati >= 0 && birimFiyati <= 50000))
            {
                return new ErrorResult("Birim fiyatı 0 ile 50000 arasında olmalıdır!");
            }
            var entity = new Urun()
            {
                Adi = model.Adi.Trim(),
                Aciklamasi = model.Aciklamasi?.Trim(),
                BirimFiyati = birimFiyati,
                StokMiktari = model.StokMiktari,
                SonKullanmaTarihi = sonKullanmaTarihi,
                KategoriId = model.KategoriId,
                CreatedBy = model.CreatedBy,
            };
            _urunRepo.Add(entity);
            model.Id = entity.Id;
            return new SuccessResult("Ürün başarıyla eklendi.");
        }

        public Result Delete(int id)
        {
            _urunRepo.DeleteEntity(id);
            return new SuccessResult("Ürün başarıyla silindi.");
        }

        public void Dispose()
        {
            _urunRepo.Dispose();
        }

        public IQueryable<UrunModel> Query()
        {
            var query = _urunRepo.EntityQuery("Kategori").OrderBy(u => u.Adi).Select(u => new UrunModel()
            {
                Id = u.Id,
                Adi = u.Adi,
                Aciklamasi = u.Aciklamasi,
                BirimFiyati = u.BirimFiyati,
                StokMiktari = u.StokMiktari,
                SonKullanmaTarihi = u.SonKullanmaTarihi,
                KategoriId = u.KategoriId,

                BirimFiyatiModel = u.BirimFiyati.ToString(new CultureInfo("tr-TR")),
                SonKullanmaTarihiModel = u.SonKullanmaTarihi.HasValue ? u.SonKullanmaTarihi.Value.ToString("yyyy-MM-dd") : "",
                KategoriModel = new KategoriModel()
                {
                    Id = u.Kategori.Id,
                    Adi = u.Kategori.Adi,
                    Aciklamasi = u.Kategori.Aciklamasi
                }
            });
            return query;
        }

        public Result Update(UrunModel model)
        {
            if (_urunRepo.EntityQuery().Any(u => u.Adi.ToLower() == model.Adi.ToLower().Trim() && u.Id != model.Id))
            {
                return new ErrorResult("Aynı ürün adına sahip kayıt bulunmaktadır!");
            }
            DateTime? sonKullanmaTarihi = string.IsNullOrWhiteSpace(model.SonKullanmaTarihiModel) ? null : DateTime.Parse(model.SonKullanmaTarihiModel, new CultureInfo("tr-TR"));
            if (sonKullanmaTarihi != null && sonKullanmaTarihi.Value <= DateTime.Today)
            {
                return new ErrorResult("Son kullanma tarihi yarın veya daha sonraki bir tarih olmalıdır!");
            }
            var entity = _urunRepo.EntityQuery().SingleOrDefault(u => u.Id == model.Id);
            entity.Adi = model.Adi.Trim();
            entity.Aciklamasi = model.Aciklamasi?.Trim();
            entity.BirimFiyati = Convert.ToDouble(model.BirimFiyatiModel.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
            entity.StokMiktari = model.StokMiktari;
            entity.SonKullanmaTarihi = sonKullanmaTarihi;
            entity.KategoriId = model.KategoriId;
            entity.UpdatedBy = model.UpdatedBy;
            _urunRepo.Update(entity);
            return new SuccessResult("Ürün başarıyla güncellendi.");
        }

        public Result<List<UrunRaporuModel>> UrunRaporuGetir(UrunRaporuFiltreModel filtre = null, PageModel sayfa = null, OrderModel sira = null)
        {
            var urunQuery = _urunRepo.EntityQuery();
            var kategoriQuery = _kategoriRepo.EntityQuery();
            var query = from k in kategoriQuery
                        join u in urunQuery
                        on k.Id equals u.KategoriId into kategoriUrunJoin
                        from subKategoriUrunJoin in kategoriUrunJoin.DefaultIfEmpty()
                            //orderby k.Adi, subKategoriUrunJoin.Adi
                        select new UrunRaporuModel()
                        {
                            BirimFiyatiModel = subKategoriUrunJoin.BirimFiyati.ToString("C2", new CultureInfo("tr-TR")),
                            CreateDateModel = subKategoriUrunJoin.CreateDate.HasValue ? subKategoriUrunJoin.CreateDate.Value.ToString(new CultureInfo("tr-TR")) : "",
                            UpdateDateModel = subKategoriUrunJoin.UpdateDate.HasValue ? subKategoriUrunJoin.UpdateDate.Value.ToString(new CultureInfo("tr-TR")) : "",
                            CreatedByModel = subKategoriUrunJoin.CreatedBy,
                            UpdatedByModel = subKategoriUrunJoin.UpdatedBy,
                            KategoriAciklamasi = k.Aciklamasi,
                            KategoriAdi = k.Adi,
                            SonKullanmaTarihiModel = subKategoriUrunJoin.SonKullanmaTarihi.HasValue ? subKategoriUrunJoin.SonKullanmaTarihi.Value.ToString("dd.MM.yyyy", new CultureInfo("tr-TR")) : "",
                            StokMiktari = subKategoriUrunJoin.StokMiktari,
                            UrunAdi = subKategoriUrunJoin.Adi,
                            KategoriId = k.Id,
                            BirimFiyat = subKategoriUrunJoin.BirimFiyati,
                            SonKullanmaTarihi = subKategoriUrunJoin.SonKullanmaTarihi
                        };
            if (sira != null)
            {
                switch (sira.Expression)
                {
                    case "Kategori Adı":
                        query = sira.DirectionAscending ? query.OrderBy(q => q.KategoriAdi) : query.OrderByDescending(q => q.KategoriAdi);
                        break;
                    case "Ürün Adı":
                        query = sira.DirectionAscending ? query.OrderBy(q => q.UrunAdi) : query.OrderByDescending(q => q.UrunAdi);
                        break;
                    case "Birim Fiyatı":
                        query = sira.DirectionAscending ? query.OrderBy(q => q.BirimFiyat) : query.OrderByDescending(q => q.BirimFiyat);
                        break;
                    case "Stok Miktarı":
                        query = sira.DirectionAscending ? query.OrderBy(q => q.StokMiktari) : query.OrderByDescending(q => q.StokMiktari);
                        break;
                    default:
                        query = sira.DirectionAscending ? query.OrderBy(q => q.SonKullanmaTarihi) : query.OrderByDescending(q => q.SonKullanmaTarihi);
                        break;
                }
            }
            if (filtre != null)
            {
                if (filtre.KategoriId != null)
                    query = query.Where(q => q.KategoriId == filtre.KategoriId);
                if (!string.IsNullOrWhiteSpace(filtre.UrunAdi))
                    query = query.Where(q => q.UrunAdi.ToUpper().Contains(filtre.UrunAdi.ToUpper().Trim()));
                if (!string.IsNullOrWhiteSpace(filtre.BirimFiyatBaslangic))
                {
                    double birimFiyatBaslangic = Convert.ToDouble(filtre.BirimFiyatBaslangic.Replace(",", "."), CultureInfo.InvariantCulture);
                    query = query.Where(q => q.BirimFiyat >= birimFiyatBaslangic);
                }
                if (!string.IsNullOrWhiteSpace(filtre.BirimFiyatBitis))
                {
                    double birimFiyatBitis = Convert.ToDouble(filtre.BirimFiyatBitis.Replace(",", "."), CultureInfo.InvariantCulture);
                    query = query.Where(q => q.BirimFiyat <= birimFiyatBitis);
                }
                if (!string.IsNullOrWhiteSpace(filtre.SonKullanmaTarihiBaslangic))
                {
                    DateTime sonKullanmaTarihiBaslangic = DateTime.Parse(filtre.SonKullanmaTarihiBaslangic, new CultureInfo("tr-TR"));
                    query = query.Where(q => q.SonKullanmaTarihi >= sonKullanmaTarihiBaslangic);
                }
                if (!string.IsNullOrWhiteSpace(filtre.SonKullanmaTarihiBitis))
                {
                    DateTime sonKullanmaTarihiBitis = DateTime.Parse(filtre.SonKullanmaTarihiBitis, new CultureInfo("tr-TR"));
                    query = query.Where(q => q.SonKullanmaTarihi <= sonKullanmaTarihiBitis);
                }
                if (filtre.StokMiktariBaslangic != null)
                {
                    query = query.Where(q => q.StokMiktari >= filtre.StokMiktariBaslangic);
                }
                if (filtre.StokMiktariBitis != null)
                {
                    query = query.Where(q => q.StokMiktari <= filtre.StokMiktariBitis);
                }
            }
            if (sayfa != null)
            {
                sayfa.RecordsCount = query.Count();
                int skip = (sayfa.PageNumber - 1) * sayfa.RecordsPerPageCount;
                int take = sayfa.RecordsPerPageCount;
                query = query.Skip(skip).Take(take);
            }
            return new SuccessResult<List<UrunRaporuModel>>(query.ToList());
        }
    }
}
