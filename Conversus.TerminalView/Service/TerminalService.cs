using System;
using Conversus.TerminalService.Contract;

namespace Conversus.TerminalView.Service
{
    public class TerminalService : ITerminalService
    {
        public void CallClient(Guid clientId)
        {
            QueueBoardWindow.CallClient(clientId);
        }
    }
}
