namespace Conversus.Storage.Impl
{
    public class ConversusDataContext : conversusEntities
    {
        public ConversusDataContext(string connectionString) : base(connectionString)
        {
        }
    }
}
