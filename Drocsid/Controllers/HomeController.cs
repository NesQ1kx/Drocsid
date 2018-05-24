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
        private readonly ITopicLogic _logic;

        public HomeController(ITopicLogic logic)
        {
            _logic = logic;
        }

        public ActionResult Index()
        {
            return View(_logic.GetForIndex());
        }
    }
}