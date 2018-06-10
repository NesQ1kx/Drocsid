using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
//using Drocsid.Json;
using Drocsid.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;

namespace Drocsid
{
    public class ChatHub : Hub
    {
        static List<User> users = new List<User>();
        static List<string> ids = new List<string> { "OdPutNzVDnQ", "lNb0VoHmWf8" };
        static string currentVideoId = "OdPutNzVDnQ";
        static string nextVideoId = "lNb0VoHmWf8";
        string apiKey = "AIzaSyBhzaHS-KBV6sNUQ-ZlkMMq7j38-CK4aPM";
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
                    string url = "https://www.googleapis.com/youtube/v3/videos?part=snippet&id=" + msg.VideoId + "&key=" + apiKey;
                     WebRequest req = WebRequest.Create(url);
                     WebResponse resp = req.GetResponse();
                     Stream stream = resp.GetResponseStream();
                     StreamReader sr = new StreamReader(stream);
                     string res = sr.ReadToEnd();
                     sr.Close();
                    /*  WebClient client = new WebClient { Encoding = Encoding.UTF8 };
                      //client.Headers.Add("user-agent", "Mozilla/5.0");
                      var res = client.DownloadString(url);*/
                    //var response = JsonConvert.DeserializeObject<JsonObj>(res); 
                    var jsonObj = JsonObj.FromJson(res);
                    Clients.All.addMessage(ser.Serialize(new
                    {
                        Type = msg.Type,
                        CurrentId = "",
                        VideoId = jsonObj.Items[0].Snippet.Title,
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
                case MessageType.VideoEnded:
                    /*int currI1 = 0;
                    for (int i = currI1; i < ids.Count; i++)
                    {
                        if (currentVideoId == ids[i])
                        {
                            currentVideoId = nextVideoId;
                            nextVideoId = ids[i + 1];
                            currI1++;
                            //break;
                        }
                    }*/

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
                    CurrentId = nextVideoId,
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