using System;

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
