using F1_Car_Garage.Models;
using F1_Car_Garage.Repository.IRepository;
using F1_Car_Garage.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace F1_Car_Garage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManufacturerController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<IdentityUser> _userManager;

        public ManufacturerController(IUnitOfWork uow, UserManager<IdentityUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var list = _uow.Manufacturers.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View(new ManufacturerViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ManufacturerViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            // Create Identity user for the manufacturer
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
            await _userManager.AddToRoleAsync(user, "Manufacturer");

            var manufacturer = new Manufacturer
            {
                Name = vm.Name,
                Country = vm.Country,
                Tier = vm.Tier,
                UserId = user.Id
            };
            _uow.Manufacturers.Add(manufacturer);
            _uow.Save();
            TempData["success"] = $"Manufacturer added! Login: {vm.Username} / {vm.Password}";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var m = _uow.Manufacturers.Get(id.Value);
            if (m == null) return NotFound();
            var vm = new ManufacturerViewModel
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                Country = m.Country,
                Tier = m.Tier
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(ManufacturerViewModel vm)
        {
            // Remove password validation for edits
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Email");

            if (!ModelState.IsValid) return View(vm);

            var existing = _uow.Manufacturers.Get(vm.ManufacturerId);
            if (existing == null) return NotFound();
            existing.Name = vm.Name;
            existing.Country = vm.Country;
            existing.Tier = vm.Tier;
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var m = _uow.Manufacturers.Get(id);
            if (m == null) return NotFound();

            // Deletes the Identity user if it exists
            if (!string.IsNullOrEmpty(m.UserId))
            {
                var user = await _userManager.FindByIdAsync(m.UserId);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            _uow.Manufacturers.Remove(m);
            _uow.Save();
            TempData["success"] = "Manufacturer deleted.";
            return RedirectToAction(nameof(Index));
        }
    }
}