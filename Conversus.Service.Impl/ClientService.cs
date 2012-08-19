using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;
using Conversus.Service.Helpers;
using Conversus.TerminalService.Contract;
using Conversus.Core.Infrastructure.Repository;

namespace Conversus.Service.Impl
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ClientService : IClientService
    {
        private IClientLogic _clientLogic;
        private IClientLogic ClientLogic
        {
            get { return _clientLogic ?? (_clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>()); }
        }

        private static IQueueLogic _queueLogic;
        private static IQueueLogic QueueLogic
        {
            get { return _queueLogic ?? (_queueLogic = BusinessLogicFactory.Instance.Get<IQueueLogic>()); }
        }

        private IOperatorLogic _userLogic;
        private IOperatorLogic UserLogic
        {
            get { return _userLogic ?? (_userLogic = BusinessLogicFactory.Instance.Get<IOperatorLogic>()); }
        }

        private ITerminalService TerminalService
        {
            get { return ServiceHelper.Instance.TerminalService; }
        }

        #region Implementation of IClientService

        public ClientInfo CreateForCommon(string name, QueueType queueType)
        {
            return ToClientInfo(ClientLogic.CreateForCommon(name, queueType));
        }

        public ClientInfo CreateFromLotus(string name, int pin, QueueType queueType, DateTime bookingTime)
        {
            return ToClientInfo(ClientLogic.CreateFromLotus(name, pin, queueType, bookingTime));
        }

        public ClientInfo GetClientByPin(int pin)
        {
            return ToClientInfo(ClientLogic.Get(new ClientFilterParameters {PIN = pin}).SingleOrDefault());
        }

        public ICollection<ClientInfo> Get(ClientFilterParameters filter)
        {
            return ClientLogic.Get(filter).Select(ToClientInfo).ToList();
        }

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public void SetAllRegisteredAsAbsent()
        {
            ClientLogic.SetAllRegisteredAsAbsent();
        }

        public ICollection<ClientInfo> GetClientsQueue(QueueType queue)
        {
            return ClientLogic
                .GetClientsQueue(queue)
                .Select(ToClientInfo)
                .ToList();
        }

        public ClientInfo CallNextClient(QueueType queue, Guid userId)
        {
            var client = ClientLogic.CallNextClient(queue, userId);
            if (client != null)
            {
                var clientInfo = ToClientInfo(client);
                TerminalService.CallClient(clientInfo);
                return clientInfo;
            }
            return null;
        }

        public ClientInfo CallClient(Guid id, Guid userId)
        {
            var client = ClientLogic.CallClient(id, userId);
            if (client != null)
            {
                var clientInfo = ToClientInfo(client);
                TerminalService.CallClient(clientInfo);
                return clientInfo;
            }
            return null;
        }

        public void Postpone(Guid id)
        {
            ClientLogic.ChangeStatus(id, ClientStatus.Postponed);
        }

        public ClientInfo CallClientByTicket(string ticket, Guid userId)
        {
            var client = ClientLogic.Get(new ClientFilterParameters { Ticket = ticket }).SingleOrDefault();
            if (client == null)
                return null;

            client = ClientLogic.CallClient(client.Id, userId);

            var info = ToClientInfo(client);
            TerminalService.CallClient(info);
            return info;
        }

        public void ChangeQueue(Guid clientId, Guid targetOperatorId, SortPriority sortPriority)
        {
            ClientLogic.ChangeQueue(clientId, targetOperatorId, sortPriority);
        }

        #endregion

        private ClientInfo ToClientInfo(IClient client)
        {
            if (client == null)
                return null;

            var queue = QueueLogic.Get(client.QueueId);

            var clientInfo = new ClientInfo
                                 {
                                     Id = client.Id,
                                     Name = client.Name,
                                     PIN = client.PIN,
                                     Status = client.Status,
                                     Ticket = client.Ticket,
                                     BookingTime = client.BookingTime,
                                     ChangeTime = client.ChangeTime,
                                     Queue = new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type)),
                                     Operator = client.OperatorId.HasValue 
                                         ? OperatorService.ToOperatorInfo(UserLogic.Get(client.OperatorId.Value))
                                         : null
                                 };

            return clientInfo;
        }
    }
}
