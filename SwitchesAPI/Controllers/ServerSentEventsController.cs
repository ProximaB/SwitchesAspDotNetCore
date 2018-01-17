using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwitchesAPI.DB.DbModels;
using SwitchesAPI.Filters;
using SwitchesAPI.Handlers.WebSocketsHandlers;
using SwitchesAPI.Interfaces;
using SwitchesAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Http.Cors;


namespace SwitchesAPI.Controllers
{
    [Route("api/[controller]")]
    public class ServerSentEventsController : Controller
    {
        private readonly ISwitchesService _switchesService;
        private readonly NotificationsMessageHandler _notificationsMessageHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerSentEventsController(IHttpContextAccessor httpContextAccessor, ISwitchesService switchesService, IRoomsService roomsServices, NotificationsMessageHandler notificationsMessageHandler)
        {
            _httpContextAccessor = httpContextAccessor;
            _switchesService = switchesService;
            _notificationsMessageHandler = notificationsMessageHandler;
        }

        private async Task chatter_PropertyChangedAsync(object sender, PropertyChangedEventArgs e)
        {
            var response = _httpContextAccessor.HttpContext.Response;

            await response
                   .WriteAsync("data: A property has changed: {e.PropertyName} \r\r");

            response.Body.Flush();
        }

        [HttpGet]
        public async Task Get()
        {
            var response = _httpContextAccessor.HttpContext.Response;
            response.Headers.Add("Content-Type", "text/event-stream");

            for (var i = 0; true; ++i)
            {

                await response
                    .WriteAsync($"data: Controller {i} at {System.DateTime.Now}\r\r");

                response.Body.Flush();
                await Task.Delay(1000);
            }
           // var _switches = AutoMapper.Mapper.Map<List<SwitchResponse>>(_switchesService.GetAll());
            //return Ok(_switches);
        }

        
    }
}
