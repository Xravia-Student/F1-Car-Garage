using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using F1_Car_Garage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace F1_Car_Garage.Controllers
{
    [Authorize(Roles = "Admin")] // Only admins can manage racers, as they are responsible for overseeing the entire garage and ensuring that racers are properly assigned to cars and have the necessary credentials to access the system. Manufacturers should not have access to racer management to maintain a clear separation of responsibilities and to prevent unauthorized modifications to racer information.
    public class RacerController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;

        public RacerController(IUnitOfWork uow, UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var racers = _uow.Racers.GetAll();
            return View(racers);
        }

        public IActionResult Upsert(int? id) // Upsert action for both creating and editing racers
        {
            ViewBag.Cars = _uow.Cars.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Model,
                    Value = c.CarId.ToString()
                });

            if (id == null || id == 0)
                return View(new RacerViewModel());

            var racer = _uow.Racers.Get(id.Value);
            if (racer == null) return NotFound();

            var vm = new RacerViewModel
            {
                RacerId = racer.RacerId,
                Name = racer.Name,
                Nationality = racer.Nationality,
                Points = racer.Points,
                CarId = racer.CarId
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken] // Handles both creation and updating of racers, with validation for new racers to ensure they have a password, and also manages the associated Identity user accounts for authentication and role assignment.
        public async Task<IActionResult> Upsert(RacerViewModel vm)
        {
            ViewBag.Cars = _uow.Cars.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Model,
                    Value = c.CarId.ToString()
                });

            if (vm.RacerId == 0 && string.IsNullOrEmpty(vm.Password))
                ModelState.AddModelError("Password", "Password is required for new racers.");

            if (!ModelState.IsValid) return View(vm);

            var racer = new Racer
            {
                RacerId = vm.RacerId,
                Name = vm.Name,
                Nationality = vm.Nationality,
                Points = vm.Points,
                CarId = vm.CarId
            };

            if (vm.RacerId == 0)
            {
                // Create Identity user for the racer Uniquely identify the racer in the system and allow them to log in and access their information, while also assigning them the "Racer" role to ensure they have the appropriate permissions within the application.
                var user = new IdentityUser
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, vm.Password!);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(vm);
                }
                await _userManager.AddToRoleAsync(user, "Racer");

                racer.UserId = user.Id;
                _uow.Racers.Add(racer);
                TempData["success"] = $"Racer added! Login: {vm.Username} / {vm.Password}";
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var racer = _uow.Racers.Get(id);
            if (racer == null) return NotFound();

            // Delete the associated Identity user if it exists
            if (!string.IsNullOrEmpty(racer.UserId))
            {
                var user = await _userManager.FindByIdAsync(racer.UserId);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            _uow.Racers.Remove(racer);
            _uow.Save();
            TempData["success"] = "Racer deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}