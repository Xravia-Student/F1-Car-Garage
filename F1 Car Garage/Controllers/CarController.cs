using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace F1_Car_Garage.Controllers
{
    public class CarController : Controller
    {
        private readonly IUnitOfWork _uow;

        public CarController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var cars = _uow.Cars.GetAll();
            return View(cars);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                return View(new Car());

            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upsert(Car car)
        {
            if (!ModelState.IsValid) return View(car);

            if (car.CarId == 0)
            {
                _uow.Cars.Add(car);
                TempData["success"] = "Car added!";
            }
            else
            {
                var existing = _uow.Cars.Get(car.CarId);
                if (existing == null) return NotFound();
                existing.Model = car.Model;
                existing.BaseSpeed = car.BaseSpeed;
                existing.BaseHandling = car.BaseHandling;
                existing.BaseAcceleration = car.BaseAcceleration;
                existing.BaseManeuverability = car.BaseManeuverability;
                TempData["success"] = "Car updated!";
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _uow.Cars.Get(id);
            if (car == null) return NotFound();
            _uow.Cars.Remove(car);
            _uow.Save();
            TempData["success"] = "Car deleted.";
            return RedirectToAction(nameof(Index));
        }

        // Details page with radar chart
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }
    }
}