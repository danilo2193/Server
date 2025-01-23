//using System;
//using System.Collections.Concurrent;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace WebSocketApiExample.Services
//{
//  public class WebSocketService
//  {
//    //private readonly ConcurrentDictionary<string, WebSocket> _connections = new();
//    private readonly ConcurrentBag<WebSocket> _connections = [];

//    public async Task AddConnection(WebSocket webSocket)
//    {
//      var connectionId = Guid.NewGuid().ToString();
//      _connections.Add(webSocket);

//      Console.WriteLine(connectionId);

//      await SendMessage(webSocket, "Welcome");

//      await ListenForMessages(webSocket);
//    }

//    public async Task SendMessageToAll(string message)
//    {
//      foreach (var socket in _connections)
//      {
//        var buffer = Encoding.UTF8.GetBytes(message);
//        await socket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
//      }
//    }

//    private async Task SendMessage(WebSocket socket, string message)
//    {
//      var buffer = Encoding.UTF8.GetBytes(message);
//      var segment = new ArraySegment<byte>(buffer);

//      if (socket.State == WebSocketState.Open)
//      {
//        await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
//      }
//    }

//    private async Task ListenForMessages(WebSocket socket)
//    {
//      var buffer = new byte[1024 * 4];

//      while (socket.State == WebSocketState.Open)
//      {
//        var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

//        if (result.MessageType == WebSocketMessageType.Close)
//        {
//          _connections.TryTake(out socket);
//          await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
//        }
//      }
//    }

//    public void RemoveConnection(WebSocket socket)
//    {
//      _connections.TryTake(out socket);
//    }
//  }
//}
