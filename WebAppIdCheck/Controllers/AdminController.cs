using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAppIdCheck.Models.ViewModels;

namespace WebAppIdCheck.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddUserToRole()
        {
            UserRoleViewModel model = new UserRoleViewModel();

            model.UserList = _userManager.Users.ToList();
            model.RoleList = _roleManager.Roles.ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);
                IdentityUser user = await _userManager.FindByIdAsync(model.UserId);

                var result = await _userManager.AddToRoleAsync(user, role.Name);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                    return View(model);
                }
            }

            return View(model);
        }
    }
}