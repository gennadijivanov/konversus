using System;
using System.Collections.Generic;
using System.Linq;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    public class ClientService : IClientService
    {
        private IClientLogic _clientLogic;
        private IClientLogic ClientLogic
        {
            get { return _clientLogic ?? (_clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>()); }
        }

        private IQueueLogic _queueLogic;
        private IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        #region Implementation of IClientService

        public ClientInfo CreateForCommon(string name, QueueType queueType)
        {
            return ToClientInfo(ClientLogic.CreateForCommon(name, queueType));
        }

        public ClientInfo CreateFromLotus(string name, int pin)
        {
            return ToClientInfo(ClientLogic.CreateFromLotus(name, pin));
        }

        public string GetTicket(Guid clientId)
        {
            return ClientLogic.GetTicket(clientId);
        }

        public ClientInfo GetClientByPin(int pin)
        {
            return ToClientInfo(ClientLogic.GetClientByPin(pin));
        }

        public void ClientGettingTicket()
        {
            throw new NotImplementedException();
            //TODO записать в базу время, когда печатается билет
            //DateTime.Now не вижу смысла передавать
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public ICollection<ClientInfo> GetClients(QueueType queue)
        {
            return ClientLogic.GetClients(queue).Select(ToClientInfo).ToList();
        }

        #endregion

        private ClientInfo ToClientInfo(IClient client)
        {
            if (client == null)
                return null;

            var queue = QueueLogic.Get(client.QueueId);

            var clientInfo = new ClientInfo()
                                 {
                                     Id = client.Id,
                                     Name = client.Name,
                                     PIN = client.PIN,
                                     Status = client.Status,
                                     Ticket = client.Ticket,
                                     BookingTime = client.BookingTime,
                                     TakeTicket = client.TakeTicket,
                                     PerformStart = client.PerformStart,
                                     PerformEnd = client.PerformEnd,
                                     Queue = new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type))
                                 };
            return clientInfo;
        }
    }
}
