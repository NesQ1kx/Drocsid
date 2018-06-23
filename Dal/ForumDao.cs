using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class ForumDao : IForumDao
    {
        public List<Topic> GetForIndex()
        {
            List<Topic> topics;
            using (var db = new SampleContext())
            {
                return topics = db.Topics.OrderByDescending(t => t.Pubdate).Take(5).ToList();
            }
        }

        public Topic GetTopic(int id)
        {
            Topic topic;
            using(var db = new SampleContext())
            {
                return topic = db.Topics.Find(id);
            }
        }

        public List<Comment> GetComments(int id)
        {
            List<Comment> comments;
            using(var db = new SampleContext())
            {
                return comments = db.Comments.Where(c => c.TopicId == id).ToList();
            }
        }

        public List<Section> GetSections()
        {
            List<Section> sections;
            using(var db = new SampleContext())
            {
                sections = db.Sections.ToList();
            }

            return sections;
        }

        public List<Topic> GetTopicsShort(int id)
        {
            List<Topic> topics;
            using (var db = new SampleContext())
            {
                return topics = db.Topics.Where(t => t.SectionId == id).Take(3).ToList();
            }
        }

        public List<Topic> GetAllTopics(int id)
        {
            List<Topic> topics;
            using (var db = new SampleContext())
            {
                return topics = db.Topics.Where(t => t.SectionId == id).ToList();
            }
        }

        public void AddComment(Comment comment)
        {
            using (var db = new SampleContext())
            {
                db.Comments.Add(comment);
                db.SaveChanges();
            }
        }

       public void AddTopic(Topic topic)
        {
            using (var db = new SampleContext())
            {
                db.Topics.Add(topic);
                db.SaveChanges();
            }
        }

        public User GetUser(int id)
        {
            User user;
            using (var db = new SampleContext())
            {
                return user = db.Users.Find(id);
            }
        }

        public void UpdateMessages(int id)
        {
            User user;
            using (var db = new SampleContext())
            {
                user = db.Users.Find(id);
                user.Messages += 1;
                db.SaveChanges();
            }
        }

        public List<Topic> GetTopics()
        {
            List<Topic> topics;
            using (var db = new SampleContext())
            {
                return topics = db.Topics.ToList();
            }
        }

        public Section GetSection(int id)
        {
            Section section;
            using (var db = new SampleContext())
            {
                return section = db.Sections.Find(id);
            }
        }

        public void DeleteTopic(int id)
        {
            using (var db = new SampleContext())
            {
                Topic topic = db.Topics.Find(id);
                List<Comment> comments = db.Comments.Where(c => c.TopicId == id).ToList();
                foreach(var c in comments)
                {
                    db.Comments.Remove(c);
                }
                db.Topics.Remove(topic);
                db.SaveChanges();
            }
        }

        public void DeleteComment(int id)
        {
            using (var db = new SampleContext())
            {
                Comment comment = db.Comments.Find(id);
                db.Comments.Remove(comment);
                db.SaveChanges();
            }
        }

        public void AddSection(Section section)
        {
            using (var db = new SampleContext())
            {
                db.Sections.Add(section);
                db.SaveChanges();
            }
        }
    }
}
