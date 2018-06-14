using DalContracts;
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

        public void AddComment(int topicId, string topicName, int authorId, string text)
        {
            User user = _forumDao.GetUser(authorId);
            Comment comment = new Comment()
            {
                TopicName = topicName,
                TopicId = topicId,
                AuthorId = authorId,
                Author = user.Username,
                Text = text,
                Pubdate = DateTime.Now
            };
            _forumDao.AddComment(comment);
            _forumDao.UpdateMessages(authorId);
        }

       public void AddTopic(int sectionId, int authorId, string topicName, string text)
        {
            User user = _forumDao.GetUser(authorId);
            Topic topic = new Topic()
            {
                TopicName = topicName,
                SectionId = sectionId,
                Text = text,
                AuthorId = authorId,
                Author = user.Username,
                Email = user.Email,
                Pubdate = DateTime.Now
                
            };

            _forumDao.AddTopic(topic);
            _forumDao.UpdateMessages(authorId);
        }

        public User GetUser(int id)
        {
            return _forumDao.GetUser(id);
        }

        public string GetUserTime(int id)
        {
            User user = _forumDao.GetUser(id);
            TimeSpan time = (DateTime.Now.Subtract(user.RegDate));
            string days = time.ToString("%d");
            string hours = time.ToString("%h");
            string minutes = time.ToString("%m");
            string userTime = $"{days}д {hours}ч {minutes}м";
            return userTime;
        }

        public bool SectionExist(Section section)
        {
            bool exist = false;
            var sections = _forumDao.GetSections();
            foreach(var s in sections)
            {
                if (section.Id == s.Id)
                {
                    exist = true;
                    break;
                }
            }
            return exist;

        }

        public bool TopicExist(int id)
        {
            var topics = _forumDao.GetTopics();
            foreach(var t in topics)
            {
                if (t.Id == id) return true;
            }
            return false;
        }

        public Section GetSection(int id)
        {
            return _forumDao.GetSection(id);
        }

        public void DeleteTopic(int id)
        {
            _forumDao.DeleteTopic(id);
        }

        public void DeleteComment(int id)
        {
            _forumDao.DeleteComment(id);
        }

    }
}
