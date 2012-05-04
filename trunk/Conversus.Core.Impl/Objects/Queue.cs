using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;

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

        // data from ctor params
        public Queue():this(new QueueData()
            {
                Id = 0,
                Type = QueueType.Type1
            })
        {
        }

        internal QueueData GetData()
        {
            return _data;
        }

        // from repository
        public ICollection<IClient> GetClients()
        {
            throw new NotImplementedException();
        }
    }
}
