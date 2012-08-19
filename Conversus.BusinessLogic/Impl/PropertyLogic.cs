using Conversus.Storage;

namespace Conversus.BusinessLogic.Impl
{
    public class PropertyLogic : IPropertyLogic
    {
        private readonly IPropertyStorage Storage = StorageLogicFactory.Instance.Get<IPropertyStorage>();

        public string GetProperty(string key)
        {
            return Storage.GetProperty(key);
        }

        public void SetProperty(string key, string value)
        {
            Storage.SetProperty(key, value);
        }
    }
}