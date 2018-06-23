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
        void AddComment(int topicId, string topicName, int authorId, string text);
        void AddTopic(int sectionId, int authorId, string topicName, string text);
        User GetUser(int id);
        string GetUserTime(int id);
        bool SectionExist(Section section);
        bool TopicExist(int id);
        Section GetSection(int id);
        void DeleteTopic(int id);
        void DeleteComment(int id);
        void AddSection(string sectionName);
    }
}
