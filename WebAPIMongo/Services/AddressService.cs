using System.Collections.Generic;
using MongoDB.Driver;
using WebAPIMongo.Models;
using WebAPIMongo.Utils;

namespace WebAPIMongo.Services
{
    public class AddressService
    {
        private readonly IMongoCollection<Address> _address;

        public AddressService(IDataBaseSettings settings)
        {
            var address = new MongoClient(settings.ConnectionString);
            var database = address.GetDatabase(settings.DataBaseName);
            _address = database.GetCollection<Address>(settings.AddressCollectionName);

        }
        public Address Create(Address address)
        {
            _address.InsertOne(address);
            return address;
        }
        public List<Address> Get() => _address.Find(address => true).ToList();
        public Address Get(string ID) => _address.Find<Address>(addres => addres.Id == ID).FirstOrDefault();
        public void Update(string ID, Address AddressIN)
        {
            _address.ReplaceOne(address => address.Id == ID, AddressIN);
        }
        public void Remove(Address AddressIN) => _address.DeleteOne(address => address.Id == address.Id);








    }
}
