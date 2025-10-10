using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.BL;
using OnlineExamSystem.Models;
namespace OnlineExamSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserAccountsController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public UserAccountsController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult CreateUser()
        {
            return View(new UserModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Save(UserModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateUser", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var MyUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(MyUser, "Student");

                    return RedirectToAction( "Index", "Home", new { area = "Admin" });

                }
                else
                {
                    return View("CreateUser", model);
                }
            }
            catch
            {
                return Redirect("/Error/E500");
            }
        }

    }
}
