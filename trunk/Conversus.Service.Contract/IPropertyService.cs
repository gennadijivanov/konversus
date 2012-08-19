using System.ServiceModel;

namespace Conversus.Service.Contract
{
    [ServiceContract]
    public interface IPropertyService
    {
        [OperationContract]
        string GetProperty(string key);

        [OperationContract]
        void SetProperty(string key, string value);
    }
}