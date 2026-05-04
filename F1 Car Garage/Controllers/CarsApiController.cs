using F1_Car_Garage.Data;
using F1_Car_Garage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace F1_Car_Garage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CarsApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Cars api endpoints for fetching car stats and details
        [HttpGet]
        public IActionResult GetAllCars()
        {
            var cars = _db.Cars
                .Include(c => c.CarParts)
                    .ThenInclude(cp => cp.Part)
                .Select(c => new
                {
                    c.CarId,
                    c.Model,
                    Speed = c.BaseSpeed + c.CarParts.Sum(cp => cp.Part != null ? cp.Part.SpeedBoost : 0),
                    Handling = c.BaseHandling + c.CarParts.Sum(cp => cp.Part != null ? cp.Part.HandlingBoost : 0),
                    Acceleration = c.BaseAcceleration + c.CarParts.Sum(cp => cp.Part != null ? cp.Part.AccelBoost : 0),
                    Maneuverability = c.BaseManeuverability + c.CarParts.Sum(cp => cp.Part != null ? cp.Part.ManeuverabilityBoost : 0),
                    PartCount = c.CarParts.Count
                })
                .ToList();

            return Ok(cars);
        }

        // stats endpoint to get detailed stats for a specific car, including installed parts and their contributions
        [HttpGet("{id}/stats")]
        public IActionResult GetCarStats(int id)
        {
            var car = _db.Cars
                .Include(c => c.CarParts)
                    .ThenInclude(cp => cp.Part)
                        .ThenInclude(p => p != null ? p.Manufacturer : null)
                .FirstOrDefault(c => c.CarId == id);

            if (car == null) return NotFound();

            var stats = new
            {
                car.CarId,
                car.Model,
                Speed = car.BaseSpeed + car.CarParts.Sum(cp => cp.Part != null ? cp.Part.SpeedBoost : 0),
                Handling = car.BaseHandling + car.CarParts.Sum(cp => cp.Part != null ? cp.Part.HandlingBoost : 0),
                Acceleration = car.BaseAcceleration + car.CarParts.Sum(cp => cp.Part != null ? cp.Part.AccelBoost : 0),
                Maneuverability = car.BaseManeuverability + car.CarParts.Sum(cp => cp.Part != null ? cp.Part.ManeuverabilityBoost : 0),
                InstalledParts = car.CarParts.Select(cp => new
                {
                    Name = cp.Part != null ? cp.Part.Name : "",
                    Type = cp.Part != null ? cp.Part.Type : "",
                    cp.InstalledOn
                })
            };

            return Ok(stats);
        }
    }
}