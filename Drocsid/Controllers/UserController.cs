using Drocsid.Models;
using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Drocsid.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserLogic _logic;
        private readonly IForumLogic _flogic;
        private static int _userId;

        public UserController(IUserLogic logic, IForumLogic flogic)
        {
            _logic = logic;
            _flogic = flogic;
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(SignupModel model)
        {
            if(ModelState.IsValid)
            {
                if (!_logic.CheckUserReg(model.Username))
                {
                    MailAddress from = new MailAddress("noreplydrocsid@gmail.com", "Registration");
                    MailAddress to = new MailAddress(model.Email);
                    MailMessage msg = new MailMessage(from, to);
                    msg.Subject = "Подтверждение регистрации";
                    msg.Body = string.Format("Для завершения регистрации перейдите по ссылке: " + "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>", Url.Action("ConfirmEmail", "User", new { userName = model.Username, email = model.Email }, Request.Url.Scheme));
                    msg.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("noreplydrocsid@gmail.com", "Drocsid_2018");
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                    _logic.AddUser(model.Username, CreatePasswordHash(model.Password /*_logic.GenerateSalt(model.Password.Length)*/), model.Email);
                    return RedirectToAction("Confirm", "User", new { email = model.Email });
                } else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
            }

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            Entities.User user = _logic.GetUserByLogin(model.Username);
            if (ModelState.IsValid)
            {
                if (_logic.CheckUser(model.Username, CreatePasswordHash(model.Password)))
                {
                    if(!user.IsBaned)
                    {
                        FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    } else
                    {
                        ModelState.AddModelError("", "Учётная запись заблокирована");
                    }
                } else
                {   
                    ModelState.AddModelError("", "Неверный логин/пароль/почта не подтверждена");
                }
            }

            return View(model);
        }

        public string Confirm(string email)
        {
            return "На почтовый адрес " + email + " были высланы дальнейшие инструкции по завершению регистрации";
        }

        public ActionResult ConfirmEmail(string userName, string email)
        {
            _logic.ConfirmEmail(userName);
            return RedirectToAction("Login", "User");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile(string userName)
        {
            ViewBag.IsAdmin = IsAdmin();
            Entities.User user = _logic.GetUserByLogin(userName);
            if(user != null)
            {
                ViewBag.Time = _flogic.GetUserTime(user.Id);
                return View(_logic.GetUser(user.Id));
            } else
            {
                return new HttpStatusCodeResult(404, "User Not Found");
            }
          
        }

        public ActionResult UserActivity(string userName, string email)
        {
            ViewBag.Email = email;
            return PartialView("_UserActivityPartial", _logic.GetUserComments(userName));
        }

        #region hash
        public virtual string CreatePasswordHash(string password, /*string saltkey,*/ string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string hashedPassword =
#pragma warning disable CS0618 // Тип или член устарел
                FormsAuthentication.HashPasswordForStoringInConfigFile(
                    password, passwordFormat);
#pragma warning restore CS0618 // Тип или член устарел
            return hashedPassword;
        }
        #endregion hash

        public ActionResult EditProfile(int id)
        {
            _userId = id;
            ViewBag.Id = id;
            if (_logic.UserExist(id))
            {
                return View();
            }
            else return new HttpStatusCodeResult(404, "User Not Found");
        }

        [HttpPost]
        public ActionResult EditPassword(EditModel model)
        {
            if (!String.IsNullOrWhiteSpace(model.Password))
            {
                if(model.Password.Length >= 6)
                {
                    _logic.EditPassword(model.Id, CreatePasswordHash(model.Password));
                    return RedirectToAction("EditProfile", new { id = _userId });
                }
            } 
            return RedirectToAction("EditProfile", new { id = _userId });
        }

        [HttpPost]
        public ActionResult EditName(EditModel model)
        {
            if(!String.IsNullOrWhiteSpace(model.Username))
            {
                if(model.Username.Length >= 3)
                {
                    if(!_logic.CheckUserReg(model.Username))
                    {
                        _logic.EditName(model.Id, model.Username);
                        return RedirectToAction("Logout");
                    } 
                }
            }
            return RedirectToAction("EditProfile", new { id = _userId });
        }

        public ActionResult UnbanUser(int id, string username)
        {
            _logic.UnbanUser(id);
            return RedirectToAction("Profile", "User", new { userName = username });
        }

        public ActionResult BanUser(int id, string username)
        {
            _logic.BanUser(id);
            return RedirectToAction("Profile", "User", new { userName = username });

        }

        [NonAction]
        public bool IsAdmin()
        {
            Entities.User user = _logic.GetUserByLogin(User.Identity.Name);
            if (user == null) return false;
            else if (user.Role == "admin") return true;
            else return false;
        }

    }
}