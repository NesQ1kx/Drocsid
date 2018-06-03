using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Drocsid.Models;
using Microsoft.AspNet.SignalR;

namespace Drocsid
{
    public class ChatHub : Hub
    {
        static List<User> users = new List<User>();
        static List<string> ids = new List<string> { "OdPutNzVDnQ", "lNb0VoHmWf8" };
        static string currentVideoId = "OdPutNzVDnQ";
        static string nextVideoId = "lNb0VoHmWf8";
        private JavaScriptSerializer ser = new JavaScriptSerializer();

        public void Send(string message)
        {
            var msg = ser.Deserialize<MessageModel>(message);
            switch(msg.Type)
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
                case MessageType.Play:
                    ids.Add(msg.VideoId);
                    Clients.All.addMessage(ser.Serialize(new
                    {
                        Type = msg.Type,
                        CurrentId = "",
                        VideoId = msg.VideoId,
                        NextId = "",
                        Value = "",
                        Username = msg.Username
                    }));
                    break;
                case MessageType.Skip:
                    int currI = 0;
                    for (int i = currI; i < ids.Count; i++)
                    {
                        if (currentVideoId == ids[i])
                        {
                            currentVideoId = nextVideoId;
                            nextVideoId = ids[i + 1];
                            currI++;
                            //break;
                        }
                    }
                   
                    Clients.All.addMessage(ser.Serialize(new
                    {
                        Type = msg.Type,
                        CurrentId = "",
                        VideoId = "",
                        NextId = nextVideoId,
                        Value = "",
                        Username = msg.Username
                    }));
                    break;

            }
           // Clients.All.addMessage(message);
        }


        public void Connect(string userName)
        {
            var id = Context.ConnectionId;

            if(!users.Any(x => x.ConnectionId == id))
            {
                users.Add(new User { ConnectionId = id, Name = userName });
                Clients.Caller.onConnected(ser.Serialize(new
                {
                    Type = MessageType.Message,
                    CurrentId = currentVideoId,
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
            if(item != null)
            {
               users.Remove(item);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}