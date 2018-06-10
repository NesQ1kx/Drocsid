﻿using DalContracts;
using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicCore
{
    public class ForumLogic : IForumLogic
    {
        private readonly IForumDao _forumDao;
        public ForumLogic(IForumDao forumDao)
        {
            _forumDao = forumDao;
        }
        public List<Topic> GetForIndex() => _forumDao.GetForIndex();
        

        public Topic GetTopic(int id) => _forumDao.GetTopic(id);
        
        public List<Comment> GetComments(int id) => _forumDao.GetComments(id);
        

        public List<Section> GetSections() => _forumDao.GetSections();
        

        public List<Topic> GetTopicsShort(int id) => _forumDao.GetTopicsShort(id);
        

        public List<Topic> GetAllTopics(int id) => _forumDao.GetAllTopics(id);

        public void AddComment(int topicId, string topicName, string author, string text)
        {
            Comment comment = new Comment()
            {
                TopicName = topicName,
                TopicId = topicId,
                Author = author,
                Text = text,
                Pubdate = DateTime.Now
            };
            _forumDao.AddComment(comment);
            _forumDao.UpdateMessages(author);
        }

       public void AddTopic(int sectionId, string author, string topicName, string text)
        {
            User user = _forumDao.GetUser(author);
            Topic topic = new Topic()
            {
                TopicName = topicName,
                SectionId = sectionId,
                Text = text,
                Author = author,
                Email = user.Email,
                Pubdate = DateTime.Now
                
            };

            _forumDao.AddTopic(topic);
            _forumDao.UpdateMessages(author);
        }

        public User GetUser(string userName)
        {
            return _forumDao.GetUser(userName);
        }

        public string GetUserTime(string userName)
        {
            User user = _forumDao.GetUser(userName);
            TimeSpan time = (DateTime.Now.Subtract(user.RegDate));
            string days = time.ToString("%d");
            string hours = time.ToString("%h");
            string minutes = time.ToString("%m");
            string userTime = $"{days}д {hours}ч {minutes}м";
            return userTime;
        }

    }
}
