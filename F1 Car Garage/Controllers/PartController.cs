using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1_Car_Garage.Controllers
{
    public class PartController : Controller
    {
        private readonly IUnitOfWork _uow;

        public PartController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var parts = _uow.Parts.GetAllWithManufacturer();
            return View(parts);
        }

        public IActionResult Upsert(int? id)
        {
            ViewBag.Manufacturers = _uow.Manufacturers.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.ManufacturerId.ToString()
                });

            if (id == null || id == 0)
                return View(new Part());

            var part = _uow.Parts.Get(id.Value);
            if (part == null) return NotFound();
            return View(part);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(Part part)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Manufacturers = _uow.Manufacturers.GetAll()
                    .Select(m => new SelectListItem
                    {
                        Text = m.Name,
                        Value = m.ManufacturerId.ToString()
                    });
                return View(part);
            }

            if (part.PartId == 0)
            {
                _uow.Parts.Add(part);
                TempData["success"] = "Part added!";
            }
            else
            {
                var existing = _uow.Parts.Get(part.PartId);
                if (existing == null) return NotFound();
                existing.Name = part.Name;
                existing.Type = part.Type;
                existing.Cost = part.Cost;
                existing.SpeedBoost = part.SpeedBoost;
                existing.HandlingBoost = part.HandlingBoost;
                existing.AccelBoost = part.AccelBoost;
                existing.ManeuverabilityBoost = part.ManeuverabilityBoost;
                existing.ManufacturerId = part.ManufacturerId;
                TempData["success"] = "Part updated!";
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var part = _uow.Parts.Get(id);
            if (part == null)
                return Json(new { success = false, message = "Part not found." });

            _uow.Parts.Remove(part);
            _uow.Save();
            return Json(new { success = true, message = "Part deleted." });
        }

        [HttpGet("/api/parts")]
        public IActionResult GetPartsJson()
        {
            var parts = _uow.Parts.GetAllWithManufacturer()
                .Select(p => new {
                    p.PartId,
                    p.Name,
                    p.Type,
                    p.Cost,
                    p.SpeedBoost,
                    p.HandlingBoost,
                    p.AccelBoost,
                    Manufacturer = p.Manufacturer != null ? p.Manufacturer.Name : ""
                });
            return Json(parts);
        }

        public IActionResult Compare()
        {
            var parts = _uow.Parts.GetAllWithManufacturer();
            return View(parts);
        }
    }
}