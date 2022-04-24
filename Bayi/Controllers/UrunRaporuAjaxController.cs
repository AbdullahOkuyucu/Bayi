using AppCore.Business.Models.Ordering;
using AppCore.Business.Models.Paging;
using AppCore.Business.Results;
using Bayi.Models;
using Bayi.Settings;
using Business.Models.Filtre;
using Business.Servis.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeOpenXml;

namespace Bayi.Controllers
{
    [Authorize(Roles = "Üretici")]
    public class UrunRaporuAjaxController : Controller
    {
        private readonly IUrunServis _urunServis;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrunRaporuAjaxController(IUrunServis urunServis, IHttpContextAccessor contextAccessor)
        {
            _urunServis = urunServis;
            _httpContextAccessor = contextAccessor;
        }

        public IActionResult Index(int? kategoriId)
        {
            var filter = new UrunRaporuFiltreModel()
            {
                KategoriId = kategoriId
            };
            var page = new PageModel() // PagedList kütüphanesi kullanılabilir NuGet'ten
            {
                RecordsPerPageCount = AppSettings.RecordsPerPageCount
            };
            var order = new OrderModel()
            {
                Expression = "Kategori Adı",
                DirectionAscending = true
            };
            var result = _urunServis.UrunRaporuGetir(filter, page, order);
            double recordsCount = page.RecordsCount;
            double recordsPerPageCount = page.RecordsPerPageCount;
            double totalPageCount = Math.Ceiling(recordsCount / recordsPerPageCount);
            List<SelectListItem> pageSelectListItems = new List<SelectListItem>();
            if (totalPageCount == 0)
            {
                pageSelectListItems.Add(new SelectListItem()
                {
                    Value = "1",
                    Text = "1"
                });
            }
            else
            {
                for (int pageNumber = 1; pageNumber <= totalPageCount; pageNumber++)
                {
                    pageSelectListItems.Add(new SelectListItem()
                    {
                        Value = pageNumber.ToString(),
                        Text = pageNumber.ToString()
                    });
                }
            }

            var viewModel = new UrunRaporuAjaxIndexViewModel()
            {
                Urunler = result.Data,
                Filtre = filter,
                Sayfalar = new SelectList(pageSelectListItems, "Value", "Text"),
                Sayfa = page,
                Sira = order
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(UrunRaporuAjaxIndexViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // sayfalama
                var page = new PageModel()
                {
                    PageNumber = viewModel.Sayfa.PageNumber,
                    RecordsPerPageCount = AppSettings.RecordsPerPageCount
                };
                var result = _urunServis.UrunRaporuGetir(viewModel.Filtre, page, viewModel.Sira);
                viewModel.Urunler = result.Data;

                double recordsCount = page.RecordsCount;
                double recordsPerPageCount = page.RecordsPerPageCount;
                double totalPageCount = Math.Ceiling(recordsCount / recordsPerPageCount);
                List<SelectListItem> pageSelectListItems = new List<SelectListItem>();
                if (totalPageCount == 0)
                {
                    pageSelectListItems.Add(new SelectListItem()
                    {
                        Value = "1",
                        Text = "1"
                    });
                }
                else
                {
                    for (int pageNumber = 1; pageNumber <= totalPageCount; pageNumber++)
                    {
                        pageSelectListItems.Add(new SelectListItem()
                        {
                            Value = pageNumber.ToString(),
                            Text = pageNumber.ToString()
                        });
                    }
                }
                viewModel.Sayfalar = new SelectList(pageSelectListItems, "Value", "Text", page.PageNumber);
                viewModel.Sayfa = page;
            }

            return PartialView("_UrunRaporu", viewModel);
        }
        public async Task Export()
        {
            try
            {
                var result = _urunServis.UrunRaporuGetir();
                if (result.Status == ResultStatus.Exception)
                    throw new Exception(result.Message);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelPackage excelPackage = new ExcelPackage();
                ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add("Ürün Raporu");

                // 1. satır: sütun başlıkları
                excelWorksheet.Cells["A1"].Value = "Kategori";
                excelWorksheet.Cells["B1"].Value = "Ürün";
                excelWorksheet.Cells["C1"].Value = "Birim Fiyat";
                excelWorksheet.Cells["D1"].Value = "Stok Miktarı";
                excelWorksheet.Cells["E1"].Value = "Son Kullanma Tarihi";

               // 2.satırdan itibaren veriler
                if (result.Data != null && result.Data.Count > 0)
                {
                    for (int row = 0; row < result.Data.Count; row++)
                    {
                        excelWorksheet.Cells["A" + (row + 2)].Value = result.Data[row].KategoriAdi;
                        excelWorksheet.Cells["B" + (row + 2)].Value = result.Data[row].UrunAdi;
                        excelWorksheet.Cells["C" + (row + 2)].Value = result.Data[row].BirimFiyatiModel;
                        excelWorksheet.Cells["D" + (row + 2)].Value = result.Data[row].StokMiktari;
                        excelWorksheet.Cells["E" + (row + 2)].Value = result.Data[row].SonKullanmaTarihiModel;
                    }

                }

                excelWorksheet.Cells["A:AZ"].AutoFitColumns();

                var excelData = excelPackage.GetAsByteArray();
                _httpContextAccessor.HttpContext.Response.Headers.Clear();
                _httpContextAccessor.HttpContext.Response.Clear();
                _httpContextAccessor.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                _httpContextAccessor.HttpContext.Response.Headers.Add("content-length", excelData.Length.ToString());
                _httpContextAccessor.HttpContext.Response.Headers.Add("content-disposition", "attachment; filename=\"UrunRaporu.xlsx\"");
                await _httpContextAccessor.HttpContext.Response.Body.WriteAsync(excelData, 0, excelData.Length);
                _httpContextAccessor.HttpContext.Response.Body.Flush();
                //_httpContextAccessor.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    }
}
