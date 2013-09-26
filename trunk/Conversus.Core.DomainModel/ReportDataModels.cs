using System;
using System.Collections.Generic;

namespace Conversus.Core.DomainModel
{
    public enum ReportType
    {
        ByQueue,
        ByOperators,
        ByClients
    }

    public class ReportByOperatorsModel
    {
        public string Name { get; set; }

        public string Queue { get; set; }

        public string Window { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Status { get; set; }
    }

    public class ReportByClientsModel
    {
        public string Name { get; set; }

        public string Queue { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Status { get; set; }

        public string PIN { get; set; }

        public string Operator { get; set; }

        public string BookingDate { get; set; }

        public string BookingTime { get; set; }
    }

    /// <summary>
    /// Отчет по услугам
    /// </summary>
    public class ReportByQueueModel
    {
        public DateTime Date { get; set; }

        public int PerformedCount { get; set; }
        public int NotPerformedCount { get; set; }
        public int AllClientsCount { get; set; }

        public int WorkplacesCount { get; set; }
        public int OperatorsCount { get; set; }

        public Dictionary<QueueType, ReportByQueueItem> DictByQueueTypes { get; set; }
    }

    public class ReportByQueueItem
    {
        // ожидание
        public TimeSpan MinWaiting { get; set; }
        public TimeSpan MaxWaiting { get; set; }
        public TimeSpan AverageWaiting { get; set; }

        // обслуживание
        public TimeSpan MinPerforming { get; set; }
        public TimeSpan MaxPerforming { get; set; }
        public TimeSpan AveragePerforming { get; set; }

        // нахождение в офисе
        public TimeSpan MinBeingInOffice { get; set; }
        public TimeSpan MaxBeingInOffice { get; set; }
        public TimeSpan AverageBeingInOffice { get; set; }
    }
}