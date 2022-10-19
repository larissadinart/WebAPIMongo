using System.Collections.Generic;
using MongoDB.Driver;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace WebAPIMongo.Services
{
    public class ClientServices
    {
        private readonly IMongoCollection<Client> _clients;
        public ClientServices(IDataBaseSettings settings)
        {
            var client= new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DataBaseName);
            _clients = database.GetCollection<Client>(settings.ClientCollectionName);
        }

        public Client Create(Client client)
        {
            _clients.InsertOne(client);
            return client;
        }

        public List<Client> Get() => _clients.Find(client => true).ToList();

        public Client Get(string id) => _clients.Find<Client>(client => client.Id == id).FirstOrDefault();

        public void Update(Client clientein, string id)
        {
            _clients.ReplaceOne(client => client.Id == id, clientein);
            Get(clientein.Id);
        }

        public void Remove(Client clienteIn) => _clients.DeleteOne(client => client.Id == clienteIn.Id);
    }
}
