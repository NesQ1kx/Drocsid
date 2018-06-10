using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IForumLogic
    {
        List<Topic> GetForIndex();
        Topic GetTopic(int id);
        List<Comment> GetComments(int id);
        List<Section> GetSections();
        List<Topic> GetTopicsShort(int id);
        List<Topic> GetAllTopics(int id);
        void AddComment(int topicId, string topicName, string author, string text);
        void AddTopic(int sectionId, string author, string topicName, string text);
        User GetUser(string userName);
        string GetUserTime(string userName);
    }
}
