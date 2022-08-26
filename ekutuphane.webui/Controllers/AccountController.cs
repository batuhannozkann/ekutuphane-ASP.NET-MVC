using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ekutuphane.webui.EmailService;
using ekutuphane.webui.Identity;
using ekutuphane.webui.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ekutuphane.webui.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager,SignInManager<User> signInManager,IEmailSender emailSender)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _emailSender=emailSender;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new User{
                FirstName=model.FirstName,
                LastName=model.LastName,
                Email=model.Email,
                UserName=model.UserName
            };
            var result=await _userManager.CreateAsync(user,model.Password);
            if(result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail","Account",new{
                    userId=user.Id,
                    token=code
                });
                await _emailSender.SendEmailAsync(model.Email,"Ekutuphane uyelik aktivasyonu",$"Ekutuphane hesabınızı onaylamak için lütfen <a href='https://elibrary.somee.com{url}'>tıklayınız</a>");
                return Redirect("/Account/Login");
            }
            foreach(var err in result.Errors)
            {
                ModelState.AddModelError("",err.Description);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl=null)
        {
            return View(new LoginModel
            {
                ReturnUrl=ReturnUrl
            });
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user=await _userManager.FindByNameAsync(model.Username);
            if(user==null)
            {
                ModelState.AddModelError("","Kullanıcı adı bulunamadı/Username is not found");
                return View(model);
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("","Hesabınız doğrulanmamış!/Your account is not verified ");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if(result.Succeeded)
            {
                return Redirect(model.ReturnUrl??"/Library/BookList");
            }
                ModelState.AddModelError("","Kullanıcı Adı veya Şifre Yanlış/Username or Password is not valid");
                 return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Library/BookList");
        }
        public async Task<IActionResult> ConfirmEmail(string userId,string token)
        {
            if(userId==null||token==null)
            {
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            var result=await _userManager.ConfirmEmailAsync(user,token);
            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public async Task<IActionResult> ForgotPasswordAsync(string email)
        {
            if(email==null)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if(user==null)
            {
                return View();
            }
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword","Account",new{
                userId=user.Id,
                token=resetToken
            });
            await _emailSender.SendEmailAsync(email,"Şifre sıfırlama isteği",$"Şifrenizi sıfırlamak için <a href='https://elibrary.somee.com{url}'>Tıklayınız</a>");
            ViewBag.Message="Şifre sıfırlama linkiniz mailinize gönderilmiştir";
            return View();
        }
        public IActionResult ResetPassword(string userId,string token)
        {
            return View(new ResetPasswordModel{
                Token=token,
                UserId=userId
            });
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if(user==null)
            {
                return RedirectToAction("Login");
            }
            var result = await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
            if(result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}