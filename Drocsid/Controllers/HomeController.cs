using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private Models.DrocsidEntities db = new Models.DrocsidEntities();

        public ActionResult Index()
        {
            var Items = db.Users;
            return View();
        }

        public ActionResult ShowUser()
        {
            var Items = db.Users;
            string result = "";
            foreach(var i in Items)
            {
                result += "<li>Name = " + i.username + "<li>";
            }
            return Content(result);
        }
    }
}