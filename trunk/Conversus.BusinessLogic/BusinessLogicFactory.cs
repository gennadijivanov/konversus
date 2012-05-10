using Conversus.Core.Infrastructure;

namespace Conversus.BusinessLogic
{
    public class BusinessLogicFactory : LogicFactory
    {
        private static LogicFactory _instance;

        private BusinessLogicFactory()
        {
        }

        public static LogicFactory Instance
        {
            get
            {
                return _instance ?? (_instance = new BusinessLogicFactory());
            }
        }
    }
}
