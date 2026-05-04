using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace F1_Car_Garage.Controllers
{
    public class LoginViewModel // Login to diffeerentiate between the login view and the user model, and to avoid exposing unnecessary properties of the user model in the login view
    {
        [Required]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        //testt persistant logins to validate log out featuere and to make sure that the user is logged out after closing the browser or not
        public bool RememberMe { get; set; }
    }

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager,
                                 UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        //kicks back user if failed to login and shows error message, and redirects to the appropriate page according to the role of the user if login is successful
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken] //security ttoken to prevent CSRF attacks
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Username!, model.Password!, model.RememberMe, false);
            // redirects roles to their appropriate pages, and if the user has multiple roles, it redirects to the first role in the list, and if the user has no roles, it redirects to the home page
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.Username!);
                var roles = await _userManager.GetRolesAsync(user!);

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                if (roles.Contains("Admin"))
                    return RedirectToAction("Index", "Manufacturer");
                if (roles.Contains("Manufacturer"))
                    return RedirectToAction("Index", "Car");
                if (roles.Contains("Racer"))
                    return RedirectToAction("Index", "Car");

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken] //security ttoken to prevent CSRF attacks
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied() // shows access denied page if the user tries to access a page that they don't have permission to access
        {
            return View();
        }
    }
}