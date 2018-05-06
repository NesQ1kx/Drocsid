using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        private readonly IUserLogic _logic;

        public UserController(IUserLogic logic)
        {
            _logic = logic;
        }
        public ActionResult Signup()
        {
            User user = new User
            {
                Username = "User1",
                Password = "1234",
                Email = "asdasd",
                ConfirmedEmail = true,
                Role = "admin",
                Status = 1,
            };

            //_logic.AddUser(user);
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}