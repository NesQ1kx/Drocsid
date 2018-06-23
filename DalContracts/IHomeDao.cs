using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface IHomeDao
    {
        List<Stream> GetStreams();
        void AddStream(Stream stream);
    }
}
