using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface ITopicDao
    {
        List<Topic> GetForIndex();
        Topic GetTopic(int id);
        List<Comment> GetComments(int id);
    }
}
