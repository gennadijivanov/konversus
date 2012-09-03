using System;
using System.Collections.Generic;

namespace Conversus.Core.DomainModel
{
    /// <summary>
    /// ����� �� �������
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
        // ��������
        public TimeSpan MinWaiting { get; set; }
        public TimeSpan MaxWaiting { get; set; }
        public TimeSpan AverageWaiting { get; set; }

        // ������������
        public TimeSpan MinPerforming { get; set; }
        public TimeSpan MaxPerforming { get; set; }
        public TimeSpan AveragePerforming { get; set; }

        // ���������� � �����
        public TimeSpan MinBeingInOffice { get; set; }
        public TimeSpan MaxBeingInOffice { get; set; }
        public TimeSpan AverageBeingInOffice { get; set; }
    }
}