using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Conversus.Core.DTO;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class ClientService : IClientService
    {
        public ClientData GetData(int value)
        {
            throw new NotImplementedException();
        }
    }
}
