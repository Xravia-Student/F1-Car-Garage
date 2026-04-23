using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1_Car_Garage.Controllers
{
    public class RacerController : Controller
    {
        private readonly IUnitOfWork _uow;

        public RacerController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var racers = _uow.Racers.GetAll();
            return View(racers);
        }

        public IActionResult Upsert(int? id)
        {
            ViewBag.Cars = _uow.Cars.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Model,
                    Value = c.CarId.ToString()
                });

            if (id == null || id == 0)
                return View(new Racer());

            var racer = _uow.Racers.Get(id.Value);
            if (racer == null) return NotFound();
            return View(racer);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(Racer racer)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cars = _uow.Cars.GetAll()
                    .Select(c => new SelectListItem
                    {
                        Text = c.Model,
                        Value = c.CarId.ToString()
                    });
                return View(racer);
            }

            if (racer.RacerId == 0)
            {
                _uow.Racers.Add(racer);
                TempData["success"] = "Racer added!";
            }
            else
            {
                var existing = _uow.Racers.Get(racer.RacerId);
                if (existing == null) return NotFound();
                existing.Name = racer.Name;
                existing.Nationality = racer.Nationality;
                existing.Points = racer.Points;
                existing.CarId = racer.CarId;
                TempData["success"] = "Racer updated!";
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var racer = _uow.Racers.Get(id.Value);
            if (racer == null) return NotFound();
            return View(racer);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var racer = _uow.Racers.Get(id);
            if (racer == null) return NotFound();
            _uow.Racers.Remove(racer);
            _uow.Save();
            TempData["success"] = "Racer deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}