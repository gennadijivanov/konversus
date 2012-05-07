using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Storage;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            BusinessLogicInitializer.Initialize();
            StorageLogicInitializer.Initialize();

            var clientLogic = BusinessLogicFactory.Instance.Get<IClientLogic>();
            clientLogic.CreateClient("Vasya", QueueType.Type1, null);
        }
    }
}
