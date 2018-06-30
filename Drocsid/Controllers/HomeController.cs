using Drocsid.Models;
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
        private readonly IUserLogic _ulogic;
        private readonly IHomeLogic _hlogic;

        public HomeController(IForumLogic logic, IUserLogic ulogic, IHomeLogic homeLogic)
        {
            _logic = logic;
            _ulogic = ulogic;
            _hlogic = homeLogic;
        }

        public ActionResult Index()
        {
            return View(_logic.GetForIndex());
        }

        public ActionResult Streams()
        {
            ViewBag.IsAdmin = IsAdmin();
            return PartialView("_StreamsPartial", _hlogic.GetStreams());
        }

        public ActionResult EditStreams()
        {
            return View(_hlogic.GetStreams());
        }

        public ActionResult DeleteStream(int id)
        {
            _hlogic.DeleteStream(id);
            return RedirectToAction("EditStreams");
        }

        [HttpGet]
        public ActionResult AddStream()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddStream(StreamModel model, HttpPostedFileBase photo)
        {
            if(ModelState.IsValid)
            {
                if(photo != null)
                {
                    string fileName = System.IO.Path.GetFileName(photo.FileName);
                    string path = "/Content/images1/" + fileName;
                    photo.SaveAs(Server.MapPath(path));
                    _hlogic.AddStream(model.ChanelName, model.Link, path, model.Category);
                    return RedirectToAction("Index");
                } else
                {
                    ModelState.AddModelError("", "Загрузите фото!!!");
                }
            }

            return View(model);
        }

        [NonAction]
        public bool IsAdmin()
        {
            Entities.User user = _ulogic.GetUserByLogin(User.Identity.Name);
            if (user == null) return false;
            else if (user.Role == "admin" || user.Role == "superAdmin") return true;
            else return false;
        }
    }
}