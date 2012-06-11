using System;
using Conversus.Core.DomainModel;
using Conversus.Impl.Objects;

namespace Conversus.Impl.Factories
{
    public class ClientFactory : IClientFactory
    {
        #region Implementation of IClientFactory

        public IClient CreateNewClient(string name, QueueType queueType, int? pin)
        {
            //TODO: Achtung!!!
            return new Client(name, queueType, DateTime.Now, pin ?? 0, ClientStatus.Registered, "");
        }

        #endregion
    }
}
