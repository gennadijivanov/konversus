using System;
using Conversus.BusinessLogic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Storage;
using Conversus.Storage.Impl;
using Conversus.Core.Infrastructure.Repository;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //BusinessLogicInitializer.Initialize();
            //StorageLogicInitializer.Initialize();

            //var clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>();
            //clientLogic.CreateClient("Vasya", QueueType.Approvement, null);

            string baseName = string.Format("base-{0}.db3", DateTime.Now.Second);

            using (var baseManager = SQLiteBaseManager.GetInstance(baseName))
            {
                var qst = new SQLiteQueueStorage(baseManager);
                var cst = new SQLiteClientStorage(baseManager);

                Guid id = Guid.NewGuid();

                qst.Create(new QueueData() { Id = id, Type = QueueType.Approvement });
                qst.Create(new QueueData() { Id = Guid.NewGuid(), Type = QueueType.Taking });
                var q = qst.Get(new QueueFilterParameters());
                //qst.Delete(id);

                var clientId = Guid.NewGuid();

                var client = new ClientData()
                {
                    Id = clientId,
                    QueueId = id,
                    Name = "Vasya",
                    PIN = 0,
                    Status = ClientStatus.Registered,
                    BookingTime = DateTime.Now
                };

                cst.Create(client);

                var queueByClient = qst.GetByClient(clientId);

                var cl = cst.Get(clientId);
            }

            File.Delete(baseName);
        }
    }
}
