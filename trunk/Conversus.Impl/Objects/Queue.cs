using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;
using Conversus.Impl.Factories;

namespace Conversus.Core.Impl.Objects
{
    public class Queue : IQueue
    {
        private QueueData _data;

        public QueueType Type
        {
            get { return _data.Type; }
            set { _data.Type = value; }
        }

        public int Id
        {
            get { return _data.Id; }
        }

        public Queue(QueueData data)
        {
            _data = data;
        }

        public Queue(QueueType type)
            : this(new QueueData { Type = type })
        {
        }

        internal QueueData GetData()
        {
            return _data;
        }

        // from repository
        public ICollection<IClient> GetClients()
        {
            return RepositoryFactory.GetClientRepository().GetClients(_data.Id);
        }
    }
}
