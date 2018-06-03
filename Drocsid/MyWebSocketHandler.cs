using Microsoft.AspNet.SignalR.WebSockets;


namespace Drocsid
{
    public class MyWebSocketHandler : WebSocketHandler
    {
        public MyWebSocketHandler(int? maxIncomingMessageSize) : base(maxIncomingMessageSize)
        {
        }
       
    }
}