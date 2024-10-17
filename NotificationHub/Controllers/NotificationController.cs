using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationHub.Hubs;

namespace NotificationHub.Controllers
{
    [Route("api/Notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationPush> _hubContext;

        public NotificationController(IHubContext<NotificationPush> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification(string message)
        {
            // Send the notification to connected clients
            await _hubContext.Clients.All.SendAsync("ReceiveNotification",message);

            return Ok();
        }
    }
}
