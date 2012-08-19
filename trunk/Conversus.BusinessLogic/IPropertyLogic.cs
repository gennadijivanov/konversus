namespace Conversus.BusinessLogic
{
    public interface IPropertyLogic
    {
        string GetProperty(string key);

        void SetProperty(string key, string value);
    }
}