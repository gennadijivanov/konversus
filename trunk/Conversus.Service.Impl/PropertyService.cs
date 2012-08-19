using System.ServiceModel;
using Conversus.BusinessLogic;
using Conversus.Service.Contract;

namespace Conversus.Service.Impl
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class PropertyService : IPropertyService
    {
        private IPropertyLogic _propertyLogic;
        private IPropertyLogic PropertyLogic
        {
            get { return _propertyLogic ?? (_propertyLogic = BusinessLogicFactory.Instance.Get<IPropertyLogic>()); }
        }

        public string GetProperty(string key)
        {
            return PropertyLogic.GetProperty(key);
        }

        public void SetProperty(string key, string value)
        {
            PropertyLogic.SetProperty(key, value);
        }
    }
}