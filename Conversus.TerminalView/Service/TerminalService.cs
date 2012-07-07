using Conversus.Service.Contract;
using Conversus.TerminalService.Contract;

namespace Conversus.TerminalView.Service
{
    public class TerminalService : ITerminalService
    {
        public void CallClient(ClientInfo client)
        {
            QueueBoardWindow.Instance.CallClient(client);
        }
    }
}
