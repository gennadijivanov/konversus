using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Conversus.BusinessLogic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Impl.Factories;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class ClientService : IClientService
    {
        private IClientLogic _clientLogic;
        private IClientLogic ClientLogic
        {
            get { return _clientLogic ?? (_clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>()); }
        }

        #region Implementation of IClientService

        public ClientData CreateForCommon(string name, QueueType queueType)
        {
            return GetData(ClientLogic.CreateForCommon(name, queueType));
        }

        public ClientData CreateFromLotus(string name, int pin)
        {
            return GetData(ClientLogic.CreateFromLotus(name, pin));
        }

        public string GetTicket(Guid clientId)
        {
            return ClientLogic.GetTicket(clientId);
        }

        public ClientData GetClientByPin(int pin)
        {
            return GetData(ClientLogic.GetClientByPin(pin));
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public ICollection<ClientData> GetClients(QueueType queue)
        {
            return ClientLogic.GetClients(queue).Select(GetData).ToList();
        }

        #endregion

        private ClientData GetData(IClient client)
        {
            if (client == null)
                return default(ClientData);
            return new ClientData()
                       {
                           Id = client.Id,
                           QueueId = client.GetQueue().Id,
                           Name = client.Name,
                           PIN = client.PIN,
                           Status = client.Status,
                           BookingTime = client.BookingTime,
                           PerformStart = client.PerformStart,
                           PerformEnd = client.PerformEnd,
                           TakeTicket = client.TakeTicket,
                           Ticket = client.Ticket
                       };
        }
    }
}
