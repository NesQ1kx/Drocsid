using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drocsid.Controllers
{
    public class ForumController : Controller
    {
        private readonly ITopicLogic _logic;

        public ForumController(ITopicLogic logic)
        {
            _logic = logic;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Section()
        {
            return View();
        }

        public ActionResult Topic(int id)
        {
            return View(_logic.GetTopic(id));
        }

        public ActionResult Comment(int id)
        {
            return PartialView("_CommentPartial", _logic.GetComments(id));
        }
    }
}