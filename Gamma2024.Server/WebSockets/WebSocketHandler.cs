using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Gamma2024.Server.WebSockets
{
    public class WebSocketHandler
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new();

        public async Task HandleWebSocket(WebSocket webSocket)
        {
            var socketId = Guid.NewGuid().ToString();
            _sockets.TryAdd(socketId, webSocket);

            try
            {
                await HandleMessages(socketId, webSocket);
            }
            finally
            {
                _sockets.TryRemove(socketId, out _);
                if (webSocket.State == WebSocketState.Open)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket fermé", CancellationToken.None);
                }
            }
        }

        private async Task HandleMessages(string socketId, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
        }

        public async Task BroadcastMessage<T>(T message)
        {
            var messageJson = JsonSerializer.Serialize(message);
            var messageBytes = Encoding.UTF8.GetBytes(messageJson);

            var deadSockets = new List<string>();

            foreach (var socket in _sockets)
            {
                try
                {
                    if (socket.Value.State == WebSocketState.Open)
                    {
                        await socket.Value.SendAsync(
                            new ArraySegment<byte>(messageBytes),
                            WebSocketMessageType.Text,
                            true,
                            CancellationToken.None
                        );
                    }
                    else
                    {
                        deadSockets.Add(socket.Key);
                    }
                }
                catch
                {
                    deadSockets.Add(socket.Key);
                }
            }

            foreach (var socketId in deadSockets)
            {
                _sockets.TryRemove(socketId, out _);
            }
        }
    }
} 