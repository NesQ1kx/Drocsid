using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface IForumDao
    {
        List<Topic> GetForIndex();
        Topic GetTopic(int id);
        List<Comment> GetComments(int id);
        List<Section> GetSections();
        List<Topic> GetTopicsShort(int id);
        List<Topic> GetAllTopics(int id);
        void AddComment(Comment comment);
        void AddTopic(Topic topic);
        User GetUser(int id);
        void UpdateMessages(int id);
        List<Topic> GetTopics();
        Section GetSection(int id);
        void DeleteTopic(int id);
        void DeleteComment(int id);
    }
}
