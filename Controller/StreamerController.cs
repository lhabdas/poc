using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Microsoft.Azure.SignalR.Samples.ChatRoom.Controller
{
    public class StreamerController : ControllerBase
    {
        private readonly IHubContext<Chat> context;

        public StreamerController(IHubContext<Chat> context)
        {
            this.context = context;
        }

        [Route("/api/test/")]
        [HttpGet]
        public void Test(string id)
        {
            Task.Run(async () =>
            {
                for (var i=1;i<1000000;i++)
                {
                    await context.Clients.Group("test").SendAsync("broadcastMessage", "stremaer", $"this is from the streamer { DateTime.Now }, current beat is {i}").ConfigureAwait(false);
                    await Task.Delay(50).ConfigureAwait(false);
                }

            });
        }

    }
}
