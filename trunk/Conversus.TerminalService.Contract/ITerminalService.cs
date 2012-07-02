using System;
using System.ServiceModel;

namespace Conversus.TerminalService.Contract
{
    [ServiceContract]
    public interface ITerminalService
    {
        [OperationContract]
        void CallClient(Guid clientId);
    }
}
