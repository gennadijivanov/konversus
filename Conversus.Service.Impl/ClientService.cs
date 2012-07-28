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

        private IUserLogic _userLogic;
        private IUserLogic UserLogic
        {
            get { return _userLogic ?? (_userLogic = BusinessLogicFactory.Instance.Get<IUserLogic>()); }
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

        public void ChangeStatus(Guid clientId, ClientStatus status)
        {
            ClientLogic.ChangeStatus(clientId, status);
        }

        public ICollection<ClientInfo> GetClientsQueue(QueueType queue)
        {
            return ClientLogic
                .GetClientsQueue(queue)
                .Select(c => ToClientInfo(c))
                .ToList();
        }

        public ClientInfo CallNextClient(QueueType queue, Guid userId)
        {
            var client = ClientLogic.CallNextClient(queue);
            if (client != null)
            {
                var clientInfo = ToClientInfo(client, userId);
                TerminalService.CallClient(clientInfo);
                return clientInfo;
            }
            return null;
        }

        public void CallClient(Guid id, Guid userId)
        {
            ClientLogic.ChangeStatus(id, ClientStatus.Performing);
            TerminalService.CallClient(ToClientInfo(ClientLogic.Get(id), userId));
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

            ClientLogic.ChangeStatus(client.Id, ClientStatus.Performing);

            var info = ToClientInfo(client, userId);
            TerminalService.CallClient(info);
            return info;
        }

        public ClientInfo ChangeQueue(Guid clientId, Guid userId)
        {
            //TODO NOT IMPLEMENTED
            throw new NotImplementedException();
        }

        #endregion

        private ClientInfo ToClientInfo(IClient client, Guid? userId = null)
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
                                     TakeTicket = client.TakeTicket,
                                     PerformStart = client.PerformStart,
                                     PerformEnd = client.PerformEnd,
                                     Queue = new QueueInfo(queue.Id, queue.Type, QueueLogic.GetTitle(queue.Type)),
                                     User = client.UserId.HasValue 
                                         ? UserService.ToUserInfo(UserLogic.Get(client.UserId.Value))
                                         : null
                                 };

            if (userId.HasValue)
                clientInfo.User = UserService.ToUserInfo(UserLogic.Get(userId.Value));

            return clientInfo;
        }
    }
}
