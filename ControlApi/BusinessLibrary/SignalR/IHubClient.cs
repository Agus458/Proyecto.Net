using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLibrary.SignalR
{
    public interface IHubClient
    {
        Task BroadcastMessage();
    }
}
