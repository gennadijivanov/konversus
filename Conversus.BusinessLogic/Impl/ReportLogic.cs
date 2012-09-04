using System;
using System.Linq;
using System.Collections.Generic;
using Conversus.Core.DomainModel;
using Conversus.Storage;

namespace Conversus.BusinessLogic.Impl
{
    public class ReportLogic : IReportLogic
    {
        private readonly IQueueStorage QueueStorage = StorageLogicFactory.Instance.Get<IQueueStorage>();

        private readonly IClientStorage ClientStorage = StorageLogicFactory.Instance.Get<IClientStorage>();

        private readonly IOperatorStorage OperatorStorage = StorageLogicFactory.Instance.Get<IOperatorStorage>();

        public List<ReportByQueueModel> GetReportByQueue(DateTime startDate, DateTime endDate)
        {
            var clients = ClientStorage.GetWithHistory(startDate, endDate);
            if (!clients.Any())
                return null;
            var operators = OperatorStorage.GetWithHistory(startDate, endDate);
            var queues = QueueStorage.Get(null).ToDictionary(k => k.Type, v => v);

            var model = new List<ReportByQueueModel>();

            DateTime date = startDate;
            while (date <= endDate)
            {
                ReportByQueueModel item = GetReportByQueue(date, queues, clients, operators);

                if (item != null)
                    model.Add(item);

                date = date.AddDays(1);
            }

            return model;
        }

        private static ReportByQueueModel GetReportByQueue(DateTime date, Dictionary<QueueType, IQueue> queues, ICollection<IClient> clients, ICollection<IOperator> operators)
        {
            var dayClients = clients.Where(c => c.ChangeTime.Date == date).ToList();
            if (!dayClients.Any())
                return null;

            var dayOperators = operators.Where(c => c.ChangeTime.Date == date).ToList();
            ILookup<Guid, IClient> dayClientsGrouping = dayClients.ToLookup(k => k.Id);
            var uniqueClients = dayClientsGrouping
                .Select(client => client.OrderByDescending(c => c.ChangeTime).First())
                .ToList();

            var item = new ReportByQueueModel
                           {
                               Date = date,
                               AllClientsCount = uniqueClients.Count(),
                               OperatorsCount = dayOperators.ToLookup(o => o.Id).Count(),
                               //TODO: attention!
                               WorkplacesCount = dayOperators.ToLookup(o => o.Id).Count(),
                               PerformedCount = uniqueClients.Count(c => c.Status == ClientStatus.Done),
                               NotPerformedCount = uniqueClients.Count(c => c.Status != ClientStatus.Done),
                               DictByQueueTypes = new Dictionary<QueueType, ReportByQueueItem>()
                           };

            foreach (QueueType queueType in queues.Keys)
            {
                ReportByQueueItem qi = GetReportByQueueItem(queues[queueType].Id, uniqueClients, dayClientsGrouping);
                item.DictByQueueTypes.Add(queueType, qi);
            }
            return item;
        }

        private static ReportByQueueItem GetReportByQueueItem(Guid queueId, IEnumerable<IClient> uniqueClients,
                                                              ILookup<Guid, IClient> dayClientsGrouping)
        {
            List<TimeSpan> waitings = new List<TimeSpan>();
            List<TimeSpan> performings = new List<TimeSpan>();
            List<TimeSpan> beingsInOffice = new List<TimeSpan>();

            foreach (IClient uniqueClient in uniqueClients)
            {
                List<IClient> clientHistory =
                    dayClientsGrouping[uniqueClient.Id].OrderBy(c => c.ChangeTime).ToList();

                TimeSpan waiting = new TimeSpan();
                TimeSpan performing = new TimeSpan();

                for (int i = 1; i < clientHistory.Count; i++)
                {
                    IClient currentHistoryItem = clientHistory[i];
                    IClient previousHistoryItem = clientHistory[i - 1];

                    if (previousHistoryItem.QueueId != queueId)
                        continue;

                    TimeSpan span = currentHistoryItem.ChangeTime.Subtract(previousHistoryItem.ChangeTime);

                    switch (previousHistoryItem.Status)
                    {
                        case ClientStatus.Waiting:
                        case ClientStatus.Postponed:
                            waiting = waiting.Add(span);
                            break;
                        case ClientStatus.Performing:
                            performing = performing.Add(span);
                            break;
                    }
                }

                if (waiting.Ticks > 0)
                    waitings.Add(waiting);

                if (performing.Ticks > 0)
                    performings.Add(performing);

                TimeSpan beingInOffice = waiting.Add(performing);

                if (beingInOffice.Ticks > 0)
                    beingsInOffice.Add(beingInOffice);
            }

            var qi = new ReportByQueueItem();
            if (waitings.Any())
            {
                qi.MinWaiting = waitings.Min();
                qi.MaxWaiting = waitings.Max();
                qi.AverageWaiting = new TimeSpan((long) waitings.Select(ts => ts.Ticks).Average());
            }
            if (performings.Any())
            {
                qi.MinPerforming = performings.Min();
                qi.MaxPerforming = performings.Max();
                qi.AveragePerforming = new TimeSpan((long) performings.Select(ts => ts.Ticks).Average());
            }
            if (beingsInOffice.Any())
            {
                qi.MinBeingInOffice = beingsInOffice.Min();
                qi.MaxBeingInOffice = beingsInOffice.Max();
                qi.AverageBeingInOffice = new TimeSpan((long) beingsInOffice.Select(ts => ts.Ticks).Average());
            }
            return qi;
        }
    }
}