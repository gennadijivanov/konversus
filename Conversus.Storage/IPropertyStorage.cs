using Conversus.Core.DomainModel;

namespace Conversus.Storage
{
    public interface IPropertyStorage : IStorage<IProperty>
    {
        string GetProperty(string key);

        void SetProperty(string key, string value);
    }
}