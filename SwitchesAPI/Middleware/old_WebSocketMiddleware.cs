﻿//using System;
//using System.Net.WebSockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Logging;
//using SwitchesAPI.Interfaces;

//namespace SwitchesAPI.Middlewares
//{
//    public class old_WebSocketMiddleware
//    {
//        private readonly ISwitchesService _switchesService;

//        private readonly RequestDelegate _next;
//        private readonly ILogger _logger;

//        public old_WebSocketMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
//        {
//            _next = next;
//            _logger = loggerFactory.CreateLogger<old_WebSocketMiddleware>();
//        }

//        public async Task Invoke(HttpContext httpContext)
//        {
//            _logger.LogInformation("Handling request: " + httpContext.Request.Path);
//            if (httpContext.Request.Path == "/ws")
//            {
//                //if (httpContext.WebSockets.IsWebSocketRequest)
//                //{
//                var webSocket = await httpContext.WebSockets.AcceptWebSocketAsync();
//                while (webSocket.State == WebSocketState.Open)
//                {
//                    var token = CancellationToken.None;
//                    var buffer = new ArraySegment<Byte>(new Byte[4096]);
//                    var received = await webSocket.ReceiveAsync(buffer, token);

//                    switch (received.MessageType)
//                    {
//                        case WebSocketMessageType.Text:
//                            var request = Encoding.UTF8.GetString(buffer.Array,
//                                                    buffer.Offset,
//                                                    buffer.Count);
//                            var type = WebSocketMessageType.Text;
//                            //var data = Encoding.UTF8.GetBytes("Echo from server :" + request);
//                            //string state = _switchesService.GetById(1).State;
//                            var state = "ON";
//                            var data = Encoding.UTF8.GetBytes(state);
//                            buffer = new ArraySegment<Byte>(data);
//                            await webSocket.SendAsync(buffer, type, true, token);
//                            break;
//                    }
//                }
//                //}
//                //else
//                //{
//                //    httpContext.Response.StatusCode = 400;
//                //}
//            }
//            else
//            {
//                await _next.Invoke(httpContext);
//            }

//            _logger.LogInformation("Finished handling request.");
//        }
//    }

//    public static class RequestLoggerExtensions
//    {
//        public static IApplicationBuilder UseWebSocketHandler(this IApplicationBuilder builder)
//        {
//            return builder.UseMiddleware<old_WebSocketMiddleware>();
//        }
//    }
//}