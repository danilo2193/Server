using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Mvc;
//using WebSocketApiExample.Services;

namespace Server.Controllers;

[ApiController, Route("[controller]")]
public class ServerController : ControllerBase
{

  //private readonly WebSocketService _webSocketService;
  //public ServerController(WebSocketService webSocketService)
  //{
  //  _webSocketService = webSocketService;
  //}

  [HttpPost("/server/ping")]
  public IActionResult Ping()
  {

    return Ok("Pong");
  }

  [HttpPost("/work/start")]
  public IActionResult Work()
  {
    Guid guid = Guid.NewGuid();
    return Ok("WorkStarted" + " " + guid);
  }

  [Route("/messages")]
  public async Task Messaging()
  {
    if (HttpContext.WebSockets.IsWebSocketRequest)
    {
      using WebSocket socket = await HttpContext.WebSockets.AcceptWebSocketAsync();

      while (true)
      {
        var message = "Welcome";
        var bytes = Encoding.UTF8.GetBytes(message);
        var segment = new ArraySegment<byte>(bytes, 0, bytes.Length);

        if (socket.State == WebSocketState.Open)
          await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        else if (socket.State == WebSocketState.Closed || socket.State == WebSocketState.Aborted)
          break;
        Thread.Sleep(2000);
      }
    }
    else
      HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
  }
}