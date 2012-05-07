using System;

namespace Conversus.Core.DomainModel
{
    public enum ClientStatus
    {
        Performing,
        Waiting,
        Registered,
        Late
    }

    public interface IClient : IEntity
    {
        string Name { get; set; }

        DateTime Deadline { get; set; }

        ClientStatus Status { get; set; }

        int PIN { get; set; }

        string Ticket { get; set; }

        IQueue GetQueue();
    }

    public interface IClientFactory
    {
        //TODO: lotus or common queue?
        IClient CreateNewClient(string name, QueueType queueType, int? pin);
    }
}
