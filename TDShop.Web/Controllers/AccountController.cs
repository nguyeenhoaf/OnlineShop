using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TDShop.Common;
using TDShop.Model.Models;
using TDShop.Web.App_Start;
using TDShop.Web.Models;

namespace TDShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "contactCaptcha", "Mã xác nhận không đúng")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var checkByMail = await UserManager.FindByEmailAsync(model.Email);
                if(checkByMail != null)
                {
                    ModelState.AddModelError("email", "Email đã tồn tại");
                    return View(model);
                }
                var checkbyUserName = await UserManager.FindByNameAsync(model.UserName);
                if (checkbyUserName != null)
                {
                    ModelState.AddModelError("username", "UserName đã tồn tại");
                    return View(model);
                }
                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    Fullname = model.FullName,
                    Address = model.Address
                };

                await UserManager.CreateAsync(user, model.Password);

                var userAfter = await UserManager.FindByEmailAsync(model.Email);
                if(userAfter!=null)
                {
                   await UserManager.AddToRolesAsync(userAfter.Id, new string[] { "User" });
                }
                ViewData["SuccessMsg"] = "Đăng ký thành công";

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/newuser.html"));
                content = content = content.Replace("{{UserName}}", userAfter.Fullname);
                content = content.Replace("{{Link}}", ConfigHelper.GetByKey("CurrentLink") + "dang-nhap.html");
                var toMail = model.Email;
                MailHelper.SendMail(toMail, "Thông tin liên hệ từ website", content);
            }
            return View();
        }
    }
}
