using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class TopicDao : ITopicDao
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
    }
}
