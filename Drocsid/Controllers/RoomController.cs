using Drocsid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Controllers
{
    public class RoomController : Controller
    {
        // GET: RadioBotRoom
        public ActionResult RadioBot ()
        {
            return View();
        }

        public ActionResult Chat(string msg)
        {
            MessageModel message = new MessageModel()
            {
                Username = User.Identity.Name,
                Message = msg
            };

            return PartialView("Message", message);
        }
    }
}