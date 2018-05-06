using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Topic
    {
        public int Id { get; set; }
        public string TopicName { get; set; }
        public int SectionId { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime Pubdate { get; set; }
    }
}
