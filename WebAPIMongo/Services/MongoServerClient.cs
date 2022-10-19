namespace WebAPIMongo.Services
{
    internal class MongoServerClient
    {
        private string connectionString;

        public MongoServerClient(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}