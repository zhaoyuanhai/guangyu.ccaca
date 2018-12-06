using CCACAWebUI.Common;
using CCACAWebUI.DB;
using CCACAWebUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CCACAWebUI.Controllers
{
    public class HelpController : BaseController
    {
        public HelpController(DbEntityContext dbEntity)
            : base(dbEntity)
        { }

        /// <summary>
        /// 登陆逻辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var _user = DbContext.Users.FirstOrDefault(u => u.UserName == userName && u.PassWord == password);
            if (_user != null)
            {
                var claims = new[] {
                        new Claim(ClaimTypes.Name, "bob"),
                        new Claim("name",_user.RealName?? ""),
                        new Claim("email",_user.Email?? ""),
                        new Claim("id",_user.ID.ToString())
                    };
                var user = new ClaimsPrincipal(
                    new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, user);

                return Json(new LoginModel() { Success = true });
            }

            var key = "用户名或密码错误";
            var word = DictCache.GetDict((int)Language, key);

            return Json(new LoginModel() { Success = false, Msg = word != null ? word.Value : key });
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ForgetPassword(string emailOrUsername)
        {
            var user = emailOrUsername.IndexOf("@") != 0 ?
                DbContext.Users.FirstOrDefault(u => u.Email == emailOrUsername) :
                DbContext.Users.FirstOrDefault(u => u.UserName == emailOrUsername);
            if (user == null)
            {
                ViewBag.ErrorMsg = "邮箱不存在！";
                return View();
            }

            string key = "验证码";
            var word1 = DictCache.GetDict((int)Language, key)?.Value ?? key;
            key = "您的验证码为";
            var word2 = DictCache.GetDict((int)Language, key)?.Value ?? key;

            string verCode = MailHelper.RandomNumber();
            HttpContext.Session.SetString("VerCode", verCode);
            MailHelper.SendMail(user.Email, word1, $"{word2}：{verCode}");
            HttpContext.Session.SetString("useremail", user.Email);
            return RedirectToAction("InputVerCode");
        }

        /// <summary>
        /// 输入验证码
        /// </summary>
        /// <returns></returns>
        public IActionResult InputVerCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult InputVerCode(string verCode)
        {
            if (string.IsNullOrWhiteSpace(verCode))
            {
                ViewBag.ErrorMsg = "验证码必填";
                return View();
            }

            if (verCode == HttpContext.Session.GetString("VerCode"))
            {
                return RedirectToAction("RePassword");
            }
            ViewBag.ErrorMsg = "验证码错误，重新输入";
            return View();
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public IActionResult RePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RePassword(string password, string rePassword)
        {
            if (password != rePassword)
            {
                var key = "两次密码输入不一致";
                ViewBag.ErrorMsg = DictCache.GetDict((int)Language, key)?.Value ?? key;
                return View();
            }

            if (password.Length < 6)
            {
                var key = "密码不能小于6位";
                ViewBag.ErrorMsg = DictCache.GetDict((int)Language, key)?.Value ?? key;
                return View();
            }

            var user = DbContext.Users.FirstOrDefault(u => u.Email == HttpContext.Session.GetString("useremail"));
            user.PassWord = password;
            DbContext.SaveChanges();
            return RedirectToAction("Index", "home");
        }

        /// <summary>
        /// 账号注册
        /// </summary>
        /// <returns></returns>
        public IActionResult RegestAccount()
        {
            return View(new AccountModel());
        }

        [HttpPost]
        public IActionResult RegestAccount(AccountModel model)
        {
            if (DbContext.Users.Any(x => x.UserName == model.UserName))
            {
                ViewBag.ErrorMsg = "用户名已存在";
                return View();
            }

            if (DbContext.Users.Any(x => x.Email == model.Email))
            {
                ViewBag.ErrorMsg = "邮箱已存在";
                return View();
            }

            DbContext.Users.Add(new T_User()
            {
                UserName = model.UserName,
                PassWord = model.Password,
                Phone = model.Phone,
                Email = model.Email,
                RealName = model.RealName,
                CreateTime = DateTime.Now
            });
            DbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        [HttpGet]
        public IActionResult IsLogin()
        {
            return Json(User.Identity.IsAuthenticated);
        }

        /// <summary>
        /// 变更字典对象
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult InitDict()
        {
            DictCache.InitWordDict();
            return Json(new { Success = true });
        }

        [HttpGet]
        public IActionResult GetWord(string word)
        {
            var dict = DictCache.GetDict((int)Language, word);
            return Json(dict == null ? word : dict.Value);
        }
    }
}
