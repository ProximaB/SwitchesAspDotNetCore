using SwitchesAPI.Managers.WebSocketManagers;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;


namespace SwitchesAPI.Handlers.WebSocketsHandlers
{
    public class BoardNotificationsHandler : WebSocketHandler
    {
        public BoardNotificationsHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var msg = message.Split(':');
            var id = msg[0];
            var state = msg[1];
            if(state == "ON" || state == "OFF")
            {
                //_context.Switches.Find(id).State = state;
                //_context.SaveChanges();
            }
           
            await SendMessageToAllAsync(message);
        }
    }
}