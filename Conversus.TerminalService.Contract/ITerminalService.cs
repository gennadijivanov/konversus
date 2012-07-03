using System;
using System.ServiceModel;
using Conversus.Service.Contract;

namespace Conversus.TerminalService.Contract
{
    [ServiceContract]
    public interface ITerminalService
    {
        [OperationContract]
        void CallClient(ClientInfo client);
    }
}
