using AppCore.Business.Results;
using Business.Models;
using Business.Servis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bayi.Controllers
{
    [Authorize(Roles = "Üretici")]
    public class RolController : Controller
    {
        private readonly IRolServis _rolServis;

        public RolController(IRolServis rolServis)
        {
            _rolServis = rolServis;
        }
        public IActionResult Index()
        {
            var query = _rolServis.Query();
            var model = query.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new RolModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RolModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _rolServis.Add(role);
                if (result.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(role);
                }
                throw new Exception(result.Message);
            }
            return View(role);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Bulunamadı");
            }

            var query = _rolServis.Query();

            var role = query.SingleOrDefault(r => r.Id == id.Value);

            if (role == null)
            {
                return View("Bulunamadı");
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RolModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _rolServis.Update(role);
                if (result.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(role);
                }

                throw new Exception(result.Message);
            }
            return View(role);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var deleteResult = _rolServis.Delete(id);
            if (deleteResult.Status == ResultStatus.Exception)
                deleteResult.Message = "An exception occured while deleting the role!";
            return Json(deleteResult.Message);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return View("Bulunamadı");
            }

            var query = _rolServis.Query();

            var role = query.SingleOrDefault(r => r.Id == id.Value);

            if (role == null)
            {
                return View("Bulunamadı");
            }
            return View(role);
        }
    }
}
