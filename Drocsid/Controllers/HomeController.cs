using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumLogic _logic;

        public HomeController(IForumLogic logic)
        {
            _logic = logic;
        }

        public ActionResult Index()
        {
            return View(_logic.GetForIndex());
        }
    }
}