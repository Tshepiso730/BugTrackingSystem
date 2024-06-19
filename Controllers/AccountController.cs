using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BugTrackingSystem.Models;
using BugTrackingSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BugTrackingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, FullName = model.FullName };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    try
                    {
                        // Determine default role or specific role based on user input
                        var defaultRole = "User"; // Default role if none selected
                        if (model.IsAdmin)
                        {
                            defaultRole = "Administrator";
                        }
                        else if (model.IsRD)
                        {
                            defaultRole = "Research and Development";
                        }
                        else if (model.IsPM)
                        {
                            defaultRole = "Project Manager";
                        }

                        // Check if the role exists, if not create it
                        if (!await _roleManager.RoleExistsAsync(defaultRole))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(defaultRole));
                        }

                        // Add user to role
                        await _userManager.AddToRoleAsync(user, defaultRole);

                        // Sign in the user after registration
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        // Redirect to a specific action after successful registration
                        return RedirectToAction(nameof(BugController.Index), "Bug");
                    }
                    catch (DbUpdateException ex)
                    {
                        // Log the exception and handle accordingly
                        ModelState.AddModelError(string.Empty, "An error occurred while saving the entity changes.");
                        // Optionally, you can also attempt to remove the user from the database context here if necessary
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed; redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutPost()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Logout));
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(BugController.Index), "Bug");
            }
        }
    }
}
