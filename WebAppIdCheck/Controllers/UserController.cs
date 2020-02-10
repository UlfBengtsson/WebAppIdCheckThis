using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppIdCheck.Models;

namespace WebAppIdCheck.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // Do note that i am using the IdentityUser class from the Identity framework,
        // If you need some custom propetys or fields, the you must create a new User class and use inheritance from the IdentityUser class
        // and replace it insted of IdentityUser in the controller(s) and the Startup file
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<IActionResult> Index()
        {
            // Nice and easy way to get the currently signed in user.
            // Note: This only works if the user is already signed in.
            IdentityUser user = await _userManager.GetUserAsync(User);

            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel input)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.GetUserAsync(User);

                user.Email = input.Email;

                IdentityResult result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to change.");
                }
            }
            return View(input);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser() { UserName = userViewModel.UserName, Email = userViewModel.Email };

                IdentityResult result = await _userManager.CreateAsync(user, userViewModel.Password);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(userViewModel);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(login.Email);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(
                        userName:           user.UserName,
                        password:           login.Password,
                        isPersistent:       false,
                        lockoutOnFailure:   true
                    );

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "User account locked out.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }

            }
            return View(login);
        }
    }
}