//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

//namespace WebSoketLayer.SoketSide
//{


//    public class WebSocketMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public WebSocketMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }
//        public async Task Invoke(HttpContext context)
//        {
//            if (!context.WebSockets.IsWebSocketRequest)
//            {
//                // Not a web socket request
//                await _next.Invoke(context);
//                return;
//            }

//            var ct = context.RequestAborted;
//            using (var socket = await context.WebSockets.AcceptWebSocketAsync())
//            {
//                for (var i = 0; i <10; i++)
//                {
//                    await SendStringAsync(socket, "ping", ct);
//                    var response = await ReceiveStringAsync(socket, ct);
//                    if (response != "pong")
//                    {
//                        await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Expected 'pong'", ct);
//                        return;
//                    }

//                    await Task.Delay(1000, ct);
//                }
//            }
//        }
//        private static Task SendStringAsync(WebSocket socket, string data, CancellationToken ct = default(CancellationToken))
//        {
//            var buffer = Encoding.UTF8.GetBytes(data);
//            var segment = new ArraySegment(buffer);
//            return socket.SendAsync(segment, WebSocketMessageType.Text, true, ct);
//        }
//        private static async Task ReceiveStringAsync(WebSocket socket, CancellationToken ct = default(CancellationToken))
//        {
//            // Message can be sent by chunk.
//            // We must read all chunks before decoding the content
//            var buffer = new ArraySegment(new byte[8192]);
//            using (var ms = new MemoryStream())
//            {
//                WebSocketReceiveResult result;
//                do
//                {
//                    ct.ThrowIfCancellationRequested();

//                    result = await socket.ReceiveAsync(buffer, ct);
//                    ms.Write(buffer.Array, buffer.Offset, result.Count);
//                }
//                while (!result.EndOfMessage);

//                ms.Seek(0, SeekOrigin.Begin);
//                if (result.MessageType != WebSocketMessageType.Text)
//                    throw new Exception("Unexpected message");

//                // Encoding UTF8: https://tools.ietf.org/html/rfc6455#section-5.6
//                using (var reader = new StreamReader(ms, Encoding.UTF8))
//                {
//                    return await reader.ReadToEndAsync();
//                }
//            }
//        }
//    }
//}
