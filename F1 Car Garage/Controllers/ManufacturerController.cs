using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace F1_Car_Garage.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IUnitOfWork _uow;

        public ManufacturerController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var list = _uow.Manufacturers.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Manufacturer m)
        {
            if (!ModelState.IsValid) return View(m);
            _uow.Manufacturers.Add(m);
            _uow.Save();
            TempData["success"] = "Manufacturer created!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var m = _uow.Manufacturers.Get(id.Value);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Manufacturer m)
        {
            if (!ModelState.IsValid) return View(m);
            _uow.Manufacturers.Remove(_uow.Manufacturers.Get(m.ManufacturerId));
            _uow.Manufacturers.Add(m);
            _uow.Save();
            TempData["success"] = "Manufacturer updated!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var m = _uow.Manufacturers.Get(id.Value);
            if (m == null) return NotFound();
            return View(m);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var m = _uow.Manufacturers.Get(id);
            if (m == null) return NotFound();
            _uow.Manufacturers.Remove(m);
            _uow.Save();
            TempData["success"] = "Manufacturer deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}