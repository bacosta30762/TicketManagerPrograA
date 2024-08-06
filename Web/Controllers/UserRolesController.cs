using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRolesController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRoleViewModels = new List<UserRoleViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoleViewModels.Add(new UserRoleViewModel
                {
                    UserId = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return View(userRoleViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageRolesViewModel
            {
                UserId = user.Id,
                Name = user.Name,
                Roles = _roleManager.Roles.Select(r => r.Name).ToList(),
                SelectedRoles = await _userManager.GetRolesAsync(user)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManageRoles(ManageRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var selectedRoles = model.SelectedRoles ?? new List<string>();
            var rolesToAdd = selectedRoles.Except(userRoles).ToList();
            var rolesToRemove = userRoles.Except(selectedRoles).ToList();

            await _userManager.AddToRolesAsync(user, rolesToAdd);
            await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

            return RedirectToAction("Index");
        }
    }
}
