using System;
using System.Collections.Generic;
using Conversus.Core.DomainModel;

namespace Conversus.BusinessLogic
{
    public interface IReportLogic
    {
        List<ReportByQueueModel> GetReportByQueue(DateTime startDate, DateTime endDate);
    }
}