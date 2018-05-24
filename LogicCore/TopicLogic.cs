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
    public class TopicLogic : ITopicLogic
    {
        private readonly ITopicDao _topicDao;
        public TopicLogic(ITopicDao topicDao)
        {
            _topicDao = topicDao;
        }
        public List<Topic> GetForIndex()
        {
            return _topicDao.GetForIndex();
        }

        public Topic GetTopic(int id)
        {
            return _topicDao.GetTopic(id);
        }

        public List<Comment> GetComments(int id)
        {
            return _topicDao.GetComments(id);
        }
    }
}
