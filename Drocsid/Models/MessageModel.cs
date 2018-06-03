using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Drocsid.Models
{
    public class MessageModel
    {
        public MessageType Type { get; set; }
        public string CurrentId { get; set; }
        public string VideoId { get; set; }
        public string NextId { get; set; }
        public string Value { get; set; }
        public string Username { get; set; }

    }
}