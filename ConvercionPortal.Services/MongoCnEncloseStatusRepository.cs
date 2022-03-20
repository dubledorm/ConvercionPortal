using ConvercionPortal.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Services
{
    public class MongoCnEncloseStatusRepository : ScopedRepository<CnEncloseStatus>, ICnEncloseStatusRepository
    {
        const string ConfigSectionName = "CainiaoStatusesDB";
        const string ConfigDBUrl = "DatabaseUrl";
        const string ConfigDBPort = "DatabasePort";
        const string ConfigDBName = "DatabaseName";
        const string EncloseAndStatusesCollectionName = "EncloseEvents";

        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<CnEncloseStatus> _collection;


        public MongoCnEncloseStatusRepository(IConfiguration configuration, ILogger<MongoCnEncloseStatusRepository> logger) : base(logger)
        {
            _client = new MongoClient(
                new MongoClientSettings()
                {
                    Server = new MongoServerAddress(configuration.GetSection(ConfigSectionName)[ConfigDBUrl], 
                                                    int.Parse(configuration.GetSection(ConfigSectionName)[ConfigDBPort])),
                    ClusterConfigurator = cb =>
                    {
                        cb.Subscribe<CommandStartedEvent>(e =>
                        {
                            _logger.LogDebug($"{e.CommandName} - {e.Command.ToJson()}");
                        });
                    }
                }

                );
            _database = _client.GetDatabase(configuration.GetSection(ConfigSectionName)[ConfigDBName]);
            _collection = _database.GetCollection<CnEncloseStatus>(EncloseAndStatusesCollectionName);
            AddScope("SearchEncloseId", ScopeByEncloseId);
            AddScope("SearchEncloseOwnerId", ScopeByEncloseOwnerId);
        }

        public CnEncloseStatus? Delete(int id, int ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CnEncloseStatus> GetAll()
        {
            var query = (from EncloseEvents in _collection.AsQueryable<CnEncloseStatus>() 
                        select EncloseEvents);
            query = (IMongoQueryable<CnEncloseStatus>)ExtendQueryByFilter(query);

            var result = from EncloseEvents in query select EncloseEvents;
            return result;
        }

        public CnEncloseStatus? GetById(int id, int ownerId)
        {
            var _encloseAndCNStatuses = new List<CnEncloseStatus>();
            var query = from EncloseEvents in _collection.AsQueryable<CnEncloseStatus>()
                        .Where(enclose => enclose.EncloseId.Equals(id))
                        .Where(enclose => enclose.EncloseOwnerId.Equals(ownerId))
                        select EncloseEvents;

            foreach (var encloseAndState in query)
                return encloseAndState;

            return null;
        }

        public CnEncloseStatus? Insert(CnEncloseStatus encloseAndCNStatus)
        {
            throw new NotImplementedException();
        }

        public CnEncloseStatus? Update(CnEncloseStatus encloseAndCNStatus)
        {
            throw new NotImplementedException();
        }



        // TODO. Возможно, что как-то можно не дублировать здесь функцию определённую в базовом классе.
        public void AddScopeRalation(string ScopeName, Func<string> PropertyGetter)
        {
            base.AddScopeRelation(ScopeName, PropertyGetter);
        }
    
        public IQueryable<CnEncloseStatus> ScopeByEncloseId(IQueryable<CnEncloseStatus> query, string value)
        {
            query = query.Where(enclose => enclose.EncloseId.Equals(value));
            return query;
        }

        public IQueryable<CnEncloseStatus> ScopeByEncloseOwnerId(IQueryable<CnEncloseStatus> query, string value)
        {
            query = query.Where(enclose => enclose.EncloseOwnerId.Equals(value));
            return query;
        }
    }
}
