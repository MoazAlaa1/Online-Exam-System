using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineExamSystem.BL;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.Controllers
{
    public class AccountController : Controller
    {
        SignInManager<ApplicationUser> _signInManager;
        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public IActionResult LogIn(string returnUrl)
        {
            UserModel model = new UserModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }
        public async Task<IActionResult> Save(UserModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        return Redirect(model.ReturnUrl);
                    }
                }
                else
                {
                    return Redirect("/Account/LogIn");
                }
            }
            catch
            {
                return Redirect("/Error/E500");
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Home/Index");
        }
    }
}
