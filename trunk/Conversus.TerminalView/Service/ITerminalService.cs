using System;
using System.ServiceModel;

namespace Conversus.TerminalView.Service
{
    [ServiceContract]
    public interface ITerminalService
    {
        [OperationContract]
        void CallClient(Guid clientId);
    }
}
