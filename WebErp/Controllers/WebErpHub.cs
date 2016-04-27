using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebErp.Controllers
{
    [HubName("WebErp")]
    public class WebErpHub:Hub
    {
    }
}