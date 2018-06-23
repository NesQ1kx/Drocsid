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
    public class HomeLogic : IHomeLogic
    {
        private readonly IHomeDao _homeDao;
        public HomeLogic(IHomeDao homeDao)
        {
            _homeDao = homeDao;
        }
        public void AddStream(string chanelName, string link, string photo, string category)
        {
            Stream stream = new Stream()
            {
                ChanelName = chanelName,
                Link = link,
                Photo = photo,
                Category = category
            };
            _homeDao.AddStream(stream);
        }

        public List<Stream> GetStreams()
        {
            return _homeDao.GetStreams();
        }
    }
}
