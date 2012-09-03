using System;
using System.Collections.Generic;
using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Core.DomainModel;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class ReportService : IReportService
    {
        private IReportLogic _reportLogic;
        private IReportLogic ReportLogic
        {
            get { return _reportLogic ?? (_reportLogic = BusinessLogicFactory.Instance.Get<IReportLogic>()); }
        }

        public List<ReportByQueueModel> GetReportByQueue(DateTime startDate, DateTime endDate)
        {
            return ReportLogic.GetReportByQueue(startDate, endDate);
        }
    }
}