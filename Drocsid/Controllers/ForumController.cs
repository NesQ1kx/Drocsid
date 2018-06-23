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
    public class ForumController : Controller
    {
        private readonly IForumLogic _logic;
        private readonly IUserLogic _ulogic;
        private static int _topicId;
        private static int _sectionId;
        private static string _sectionName;
        private static string _topicName;

        public ForumController(IForumLogic logic, IUserLogic ulogic)
        {
            _logic = logic;
            _ulogic = ulogic;
        }

        public ActionResult Index()
        {
            ViewBag.IsAdmin = IsAdmin();
            return View(_logic.GetSections());
        }

        public ActionResult Section(int id)
        {
            Section section = _logic.GetSection(id);
            
            if (section != null)
            {
                return View(section);
            }
            else
            {
                return new HttpStatusCodeResult(404, "Section Not Found");
            }
        }

        [HttpGet] 
        public ActionResult AddSection()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSection(SectionModel model)
        {
            if(ModelState.IsValid)
            {
                _logic.AddSection(model.SectionName);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult SectionTopicsShort(int id)
        {
            return PartialView("_SectionTopicShortPartial", _logic.GetTopicsShort(id));
        }

        public ActionResult SectionTopicsAll(int id)
        {
            if (IsAdmin())
            {
                ViewBag.IsAdmin = true;
            } else
            {
                ViewBag.IsAdmin = false;
            }
            return PartialView("_AllTopicsPartial", _logic.GetAllTopics(id));
        }

        public ActionResult Topic(int id)
        {
            if (_logic.TopicExist(id))
                return View(_logic.GetTopic(id));
            else return new HttpStatusCodeResult(404, "Topic Not Found");
        }

        public ActionResult Comment(int id)
        {
            if (IsAdmin()) ViewBag.IsAdmin = true;
            else ViewBag.IsAdmin = false;
            return PartialView("_CommentPartial", _logic.GetComments(id));
        }

        [HttpGet]
        public ActionResult AddComment(int id, string topicName)
        {
            _topicId = id;
            _topicName = topicName;
            return PartialView("_AddCommentPartial");
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            Entities.User user = _ulogic.GetUserByLogin(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _logic.AddComment(_topicId, _topicName, user.Id, model.Text);
                return RedirectToAction("Topic", "Forum", new { id = _topicId });
            } else
            {
                ModelState.AddModelError("", "Заполните поле");
            }
            //return RedirectToAction("Topic", "Forum", new { id = _topicId });
            return PartialView("_AddCommentPartial", model);
        }

        [HttpGet]
        public ActionResult AddTopic(int id, string name)
        {
            _sectionName = name;
            _sectionId = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTopic(TopicModel model)
        {
            Entities.User user = _ulogic.GetUserByLogin(User.Identity.Name);
            if (ModelState.IsValid)
            {
                _logic.AddTopic(_sectionId, user.Id, model.TopicName, model.Text);
                return RedirectToAction("Section", "Forum", new { id = _sectionId, sectionName = _sectionName });
            }

            return View(model);
        }

        public ActionResult UserInfo(int id)
        {
            ViewBag.Time = _logic.GetUserTime(id);
            return PartialView("_UserInfoPartial", _logic.GetUser(id));
        }

        public ActionResult DeleteTopic(int id, int sectionId)
        {
            _logic.DeleteTopic(id);
            return RedirectToAction("Section", "Forum", new { id = sectionId });
        }

        public ActionResult DeleteComment(int id, int topicId)
        {
            _logic.DeleteComment(id);
            return RedirectToAction("Topic", "Forum", new { id = topicId });
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