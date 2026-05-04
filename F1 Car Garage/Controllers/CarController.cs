using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using Microsoft.AspNetCore.Authorization;

namespace F1_Car_Garage.Controllers
{
    [Authorize] // All actions require authentication from manufacturers and admins
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

        [Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                return View(new Car());

            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manufacturer")]
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

        [Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Manufacturer")]
        public IActionResult DeleteConfirmed(int id)
        {
            var car = _uow.Cars.Get(id);
            if (car == null) return NotFound();
            _uow.Cars.Remove(car);
            _uow.Save();
            TempData["success"] = "Car deleted.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }

        public IActionResult QRCode(int id) // Generates a QR code linking to the car's stats page
        {
            var car = _uow.Cars.Get(id);
            if (car == null) return NotFound();

            var url = $"{Request.Scheme}://{Request.Host}/api/CarsApi/{id}/stats";

            using var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrData);
            var qrBytes = qrCode.GetGraphic(10);

            return File(qrBytes, "image/png");
        }

        public IActionResult QRCodeView(int? id) //helps manufacturers and admins to view the QR code in the browser before downloading it
        {
            if (id == null) return NotFound();
            var car = _uow.Cars.Get(id.Value);
            if (car == null) return NotFound();
            return View(car);
        }
    }
}