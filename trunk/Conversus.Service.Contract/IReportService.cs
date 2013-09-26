using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.Core.DomainModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IReportService
    {
        [OperationContract]
        List<ReportByQueueModel> GetReportByQueue(DateTime startDate, DateTime endDate);

        [OperationContract]
        List<ReportByOperatorsModel> GetReportByOperators(DateTime startDate, DateTime endDate);

        [OperationContract]
        List<ReportByClientsModel> GetReportByClients(DateTime startDate, DateTime endDate);
    }
}