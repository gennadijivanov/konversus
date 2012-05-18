using System;
using Conversus.BusinessLogic;
using Conversus.Core.DTO;
using Conversus.Core.DomainModel;
using Conversus.Storage;
using Conversus.Storage.Impl;

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

            RedisQueueStorage st = new RedisQueueStorage();

            Guid id = Guid.NewGuid();

            st.Create(new QueueData() {Id = id, Type = QueueType.Approvement});
            st.Update(new QueueData() {Id = id, Type = QueueType.Taking});
            var q = st.Get(id);
            st.Delete(id);
        }
    }
}
