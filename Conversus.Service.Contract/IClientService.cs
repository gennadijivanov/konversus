using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Conversus.Core.DTO;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        ClientData GetData(int value);

    }
}
