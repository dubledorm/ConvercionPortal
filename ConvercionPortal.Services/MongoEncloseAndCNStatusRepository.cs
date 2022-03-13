using ConvercionPortal.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    internal class MongoEncloseAndCNStatusRepository : ScopedRepository<EncloseAndCNStatus>, IEncloseAndCNStatusRepository
    {
        private readonly MongoClient _client;
        IMongoDatabase _database;


        public MongoEncloseAndCNStatusRepository(string mongoConnectionString, string mongoDatabaseName) : base()
        {
            _client = new MongoClient(mongoConnectionString);
            _database = _client.GetDatabase(mongoDatabaseName);
            //  AddScope("Name", ScopeByName);
            //  AddScope("Description", ScopeByDescription);
        }

        public EncloseAndCNStatus? Delete(int id, int ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EncloseAndCNStatus> GetAll(Dictionary<string, string> filter)
        {
            throw new NotImplementedException();
        }

        public EncloseAndCNStatus? GetById(int id, int ownerId)
        {
            throw new NotImplementedException();
        }

        public EncloseAndCNStatus? Insert(EncloseAndCNStatus encloseAndCNStatus)
        {
            throw new NotImplementedException();
        }

        public EncloseAndCNStatus? Update(EncloseAndCNStatus encloseAndCNStatus)
        {
            throw new NotImplementedException();
        }
    }
}
