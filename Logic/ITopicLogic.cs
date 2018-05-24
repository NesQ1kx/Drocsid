using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ITopicLogic
    {
        List<Topic> GetForIndex();
        Topic GetTopic(int id);
        List<Comment> GetComments(int id);
    }
}
