using System;
using System.Collections.Generic;
using Conversus.BusinessLogic.Impl;

namespace Conversus.BusinessLogic
{
    public static class BusinessLogicInitializer
    {
        public static void Initialize()
        {
            var logicTypes = new Dictionary<Type, object>
            {
                { typeof (IClientLogic), new ClientLogic() },
                { typeof (IQueueLogic), new QueueLogic() },
                { typeof (IOperatorLogic), new OperatorLogic() },
                { typeof (IPropertyLogic), new PropertyLogic() },
                { typeof (IReportLogic), new ReportLogic() },
            };

            BusinessLogicFactory.Instance.RegisterObjects(logicTypes);
        }
    }
}
