﻿using Drocsid.Models;
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
        private static int _topicId;
        private static int _sectionId;
        private static string _sectionName;

        public ForumController(IForumLogic logic)
        {
            _logic = logic;
        }

        public ActionResult Index()
        {
            return View(_logic.GetSections());
        }

        public ActionResult Section(int id, string sectionName)
        {
            Section section = new Section
            {
                Id = id,
                SectionName = sectionName
            };
            return View(section);
        }

        public ActionResult SectionTopicsShort(int id)
        {
            return PartialView("_SectionTopicShortPartial", _logic.GetTopicsShort(id));
        }

        public ActionResult SectionTopicsAll(int id)
        {
            return PartialView("_AllTopicsPartial", _logic.GetAllTopics(id));
        }

        public ActionResult Topic(int id)
        {
            return View(_logic.GetTopic(id));
        }

        public ActionResult Comment(int id)
        {
            return PartialView("_CommentPartial", _logic.GetComments(id));
        }

        [HttpGet]
        public ActionResult AddComment(int id)
        {
            _topicId = id;
            return PartialView("_AddCommentPartial");
        }

        [HttpPost]
        public ActionResult AddComment(CommentModel model)
        {
            
            if(ModelState.IsValid)
            {
                _logic.AddComment(_topicId, User.Identity.Name, model.Text);
                return RedirectToAction("Topic", "Forum", new { id = _topicId});
            }

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
            if(ModelState.IsValid)
            {
                _logic.AddTopic(_sectionId, User.Identity.Name, model.TopicName, model.Text);
                return RedirectToAction("Section", "Forum", new { id = _sectionId, sectionName = _sectionName });
            }

            return View(model);
        }

        public ActionResult UserInfo(string userName)
        {
            return PartialView("_UserInfoPartial", _logic.GetUser(userName));
        }
    }
}