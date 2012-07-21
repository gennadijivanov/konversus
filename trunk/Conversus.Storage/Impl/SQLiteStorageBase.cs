namespace Conversus.Storage.Impl
{
    public class SQLiteStorageBase
    {
        private readonly string _connectionString;

        public SQLiteStorageBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected ConversusDataContext GetDataContext()
        {
            return new ConversusDataContext(_connectionString);
        }
    }
}