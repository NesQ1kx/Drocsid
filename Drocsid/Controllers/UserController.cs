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
                    _logic.AddUser(model.Username, model.Password, model.Email);
                    /* FormsAuthentication.SetAuthCookie(model.Username, false);
                     return RedirectToAction("Index", "Home");*/
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
            if (ModelState.IsValid)
            {
                if (_logic.CheckUser(model.Username, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
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
            ViewBag.Time = _flogic.GetUserTime(userName);
            return View(_logic.GetUserByLogin(userName));
        }

        public ActionResult UserActivity(string userName, string email)
        {
            ViewBag.Email = email;
            return PartialView("_UserActivityPartial", _logic.GetUserComments(userName));
        } 

    }
}