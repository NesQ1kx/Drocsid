using Drocsid.Models;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace Drocsid
{
    public class ChatHub1 : Hub
    {
        static List<User> users = new List<User>();
        private JavaScriptSerializer ser = new JavaScriptSerializer();

        public void Send(string message)
        {
            var msg = ser.Deserialize<MessageModel>(message);
            switch (msg.Type)
            {
                case MessageType.Message:
                    Clients.All.addMessage(ser.Serialize(new
                    {
                        Type = msg.Type,
                        CurrentId = "",
                        VideoId = "",
                        NextId = "",
                        Value = msg.Value,
                        Username = msg.Username
                    }));
                    break;
            }

        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if (!users.Any(x => x.ConnectionId == id))
            {
                users.Add(new User { ConnectionId = id, Name = userName });
                Clients.Caller.onConnected(ser.Serialize(new
                {
                    Type = MessageType.Message,
                    CurrentId = "",
                    VideoId = "",
                    NextId = "",
                    Value = "",
                    Username = userName
                }));
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var item = users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                users.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}