using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IHomeLogic
    {
        List<Stream> GetStreams();
        void AddStream(string chanelName, string link, string photo, string category);
        void DeleteStream(int id);
    }
}
