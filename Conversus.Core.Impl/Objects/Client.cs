using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;

namespace Conversus.Core.Impl.Objects
{
    public class Client : IClient
    {
        private ClientData _data;

        public string Name
        {
            get { return _data.Name; }
            set { _data.Name = value; }
        }

        public DateTime Deadline
        {
            get { return _data.Deadline; }
            set { _data.Deadline = value; }
        }

        public ClientStatus Status
        {
            get { return _data.Status; }
            set { _data.Status = value; }
        }

        public int PIN
        {
            get { return _data.PIN; }
            set { _data.PIN = value; }
        }

        public string Ticket
        {
            get { return _data.Ticket; }
            set { _data.Ticket = value; }
        }

        public int Id
        {
            get { return _data.Id; }
        }

        // build data from ctor parameters
        public Client()
        {
            _data = new ClientData() {
                Id = 0, // !!!
                Name = "",
                Deadline=DateTime.Now,
                PIN=0,
                Status = ClientStatus.Waiting,
                Ticket=""
            };
        }

        // get from repository
        public IQueue GetQueue()
        {
            throw new NotImplementedException();
        }
    }
}
