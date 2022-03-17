using ConvercionPortal.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public class MongoEncloseAndCNStatusRepository : ScopedRepository<EncloseAndCNStatus>, IEncloseAndCNStatusRepository
    {
        const string ConfigSectionName = "CainiaoStatusesDB";
        const string ConfigDBConnectionName = "ConnectionString";
        const string ConfigDBName = "DatabaseName";
        const string EncloseAndStatusesCollectionName = "EncloseAndEvents";

        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<EncloseAndCNStatus> _collection;


        public MongoEncloseAndCNStatusRepository(IConfiguration configuration) : base()
        {
            _client = new MongoClient(configuration.GetSection(ConfigSectionName)[ConfigDBConnectionName]);
            _database = _client.GetDatabase(configuration.GetSection(ConfigSectionName)[ConfigDBName]);
            _collection = _database.GetCollection<EncloseAndCNStatus>(EncloseAndStatusesCollectionName);
            //  AddScope("Name", ScopeByName);
            //  AddScope("Description", ScopeByDescription);
        }

        public EncloseAndCNStatus? Delete(int id, int ownerId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EncloseAndCNStatus>> GetAll(Dictionary<string, string> filter)
        {
            var _encloseAndCNStatuses = new List<EncloseAndCNStatus>();
            var documentFilter = new BsonDocument();
            var people = await _collection.Find(documentFilter).ToListAsync();
           /* using (var cursor = await _collection.FindAsync(documentFilter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var encloses = cursor.Current;
                    foreach (EncloseAndCNStatus doc in encloses)
                    {
                        _encloseAndCNStatuses.Add(doc);
                    }
                }
            }*/
            return _encloseAndCNStatuses;
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
