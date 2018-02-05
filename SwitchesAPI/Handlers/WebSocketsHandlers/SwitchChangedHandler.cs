using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using SwitchesAPI.Managers.WebSocketManagers;

namespace SwitchesAPI.Handlers.WebSocketsHandlers
{
    public class SwitchChangedHandler : WebSocketHandler
    {
        public SwitchChangedHandler (WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task ReceiveAsync (WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);


        }

        public override async Task OnConnected (WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            await SendMessageToAllAsync($"{socketId} is now connected");
        }


        public enum Action
        {
            GET,
            POST,
            PUT
        };


        public async Task SendActionToAll(int switchId, SwitchChangedHandler.Action action, object bodyJson)
        {
            await SendMessageToAllAsync(switchId + ":" + Enum.GetName(typeof(SwitchChangedHandler.Action), action));
        }
    }
}
