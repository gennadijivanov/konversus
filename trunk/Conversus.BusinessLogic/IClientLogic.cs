using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Core.DTO;

namespace Conversus.BusinessLogic
{
    public interface IClientLogic
    {
        //TODO: разделять создаваемого из лотуса и по общей очереди
        /// <summary>
        /// Создание клиента
        /// </summary>
        void CreateClient(string name, QueueType queueType, int? pin);
        
        //TODO: установка номера тикета актуальна только для лотусовских. Пришедшему по общей очереди можно давать его сразу
        void SetTicket(int id);

        ClientData GetClient(int id);

        //TODO: FilterParameters!!!
        ICollection<ClientData> GetClients(int? queueId); 

        void ChangeStatus(int clientId, ClientStatus newStatus);
    }
}
