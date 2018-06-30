using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class HomeDao : IHomeDao
    {
        public void AddStream(Stream stream)
        {
            using (var db = new SampleContext())
            {
                db.Streams.Add(stream);
                db.SaveChanges();
            }
        }

        public List<Stream> GetStreams()
        {
            List<Stream> streams;
            using (var db = new SampleContext())
            {
                return streams = db.Streams.ToList();
            }
        }

        public void DeleteStream(int id)
        {
            using (var db = new SampleContext())
            {
                Stream stream = db.Streams.Find(id);
                db.Streams.Remove(stream);
                db.SaveChanges();
            }
        }
    }
}
