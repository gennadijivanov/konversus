using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.TerminalService.Contract;
using Conversus.Core.Infrastructure.Repository;

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

        private ITerminalService _terminalService;
        private ITerminalService TerminalService
        {
            get
            {
                if (_terminalService == null)
                {
                    var factory = new ChannelFactory<ITerminalService>("ITerminalService");
                    _terminalService = factory.CreateChannel();
                }
                return _terminalService;
            }
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

        public ClientInfo GetClientByPin(int pin)
        {
            return ToClientInfo(ClientLogic.Get(new ClientFilterParameters {PIN = pin}).SingleOrDefault());
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public ICollection<ClientInfo> GetClientsQueue(QueueType queue)
        {
            return ClientLogic
                .GetClientsQueue(queue)
                .Select(ToClientInfo)
                .ToList();
        }

        public ClientInfo CallNextClient(QueueType queue)
        {
            var client = ClientLogic.CallNextClient(queue);
            if (client != null)
            {
                var clientInfo = ToClientInfo(client);
                TerminalService.CallClient(clientInfo);
                return clientInfo;
            }
            return null;
        }

        public void CallClient(Guid id)
        {
            ClientLogic.ChangeStatus(id, ClientStatus.Performing);
            TerminalService.CallClient(ToClientInfo(ClientLogic.Get(id)));
        }

        public void Postpone(Guid id)
        {
            ClientLogic.ChangeStatus(id, ClientStatus.Postponed);
        }

        public ClientInfo CallClientByTicket(string ticket)
        {
            var client = ClientLogic.Get(new ClientFilterParameters { Ticket = ticket }).SingleOrDefault();
            if (client == null)
                return null;

            ClientLogic.ChangeStatus(client.Id, ClientStatus.Performing);

            var info = ToClientInfo(client);
            TerminalService.CallClient(info);
            return info;
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
