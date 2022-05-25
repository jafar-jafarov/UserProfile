using Identity.Intro.WebApplicationUI.Models.Entity.Membership;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserProfile.AppCode.Extensions;
using UserProfile.Models.FormModel;

namespace UserProfile.Controllers
{
    public class AccountController : Controller
    {
        readonly SignInManager<AppUser> signInManager;
        readonly UserManager<AppUser> userManager;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
       
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public  async Task<IActionResult> Register(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new();//improved target typing
                user.Email = model.Email;
                user.Name = model.Name;
                user.UserName = model.Email;
                user.Surname = model.Surname;

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(SignIn));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            return View(model);
        }
        public IActionResult SignIn()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginFormModel user)
        {
            if (ModelState.IsValid)
            {
                AppUser foundedUser = null;
                if (user.Username.IsEmail())
                {
                    foundedUser = await userManager.FindByEmailAsync(user.Username);

                }
                else
                {
                    foundedUser = await userManager.FindByNameAsync(user.Username);
                }
                if (foundedUser==null)
                {
                    ViewBag.Message = "İstifadəçi adınız və ya şifrənizz yanlışdır";
                    goto end;
                }
                var signInResult = await signInManager.PasswordSignInAsync(foundedUser, user.Password, true, true);
                if (!signInResult.Succeeded)
                {
                    ViewBag.Message = "İstifadəçi adınız və ya şifrənizz yanlışdır";
                    goto end;
                }
                var callBackUrl = Request.Query["ReturnUrl"];
                if (!string.IsNullOrWhiteSpace(callBackUrl))
                {
                    return Redirect(callBackUrl);
                }
                return RedirectToAction("Index", "PersonalSide");
            }
            end:
            return View(user);
        }
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
       
    }
}
