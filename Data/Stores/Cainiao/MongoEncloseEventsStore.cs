using ConvercionPortal.Services;
using Data.Models.Cainiao;
using Data.Stores.Cainiao;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data.Stores.Cainiao
{
    public class MongoEncloseEventsStore : ScopedRepository<EncloseEvent>, IEncloseEventsStore
    {
        const string CollectionName = "EncloseEvents";

        private readonly ILogger<MongoEncloseEventsStore> _logger;
        private readonly IMongoCollection<EncloseEvent> _encloseEvents;
        private FilterDefinition<EncloseEvent> Filter;

        public MongoEncloseEventsStore(IMongoDatabase mongoDB,
            ILogger<MongoEncloseEventsStore> logger)
        {
            _encloseEvents = mongoDB.GetCollection<EncloseEvent>(CollectionName);
            _logger = logger;
            AddScope("SearchEncloseId", ScopeByEncloseId);
            AddScope("SearchEncloseOwnerId", ScopeByEncloseOwnerId);
            AddScope("SearchTroubleFlag", ScopeByTroubleFlag);
            AddScope("SearchFinishedFlag", ScopeByFinishedFlag);
        }


        public async Task<EncloseEvent> CreateAsync(EncloseEvent encloseEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<EncloseEvent> Delete(int encloseId, int encloseOwnerID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EncloseEvent>> GetAsync(int skip = 0)
        {
            Filter = new BsonDocument();
            ExtendFilter();
            return await _encloseEvents.Find(Filter).ToListAsync();
        }

        public async Task<long> CountAsync()
        {
            Filter = new BsonDocument();
            ExtendFilter();
            return _encloseEvents.CountDocuments(Filter);
        }

        public async Task<EncloseEvent ?> GetByIdAsync(int encloseId, int encloseOwnerID)
        {
            var encloseEvent = await _encloseEvents.Find(x => x.EncloseId == encloseId
                                                             && x.EncloseOwnerId == encloseOwnerID)
                                                  .FirstOrDefaultAsync();
            return encloseEvent;
        }

        public async Task UpdateAsync(EncloseEvent encloseEvent)
        {
            await _encloseEvents.ReplaceOneAsync(x => x.EncloseId == encloseEvent.EncloseId
                                                     && x.EncloseOwnerId == encloseEvent.EncloseOwnerId, encloseEvent);
        }


        // TODO. Возможно, что как-то можно не дублировать здесь функцию определённую в базовом классе.
        public void AddScopeRelation(string ScopeName, Func<string> PropertyGetter)
        {
            base.AddScopeRelation(ScopeName, PropertyGetter);
        }

        public bool ScopeByEncloseId(string value)
        {
            try
            {
                Filter &= new BsonDocument("enclose_id", Int64.Parse(value));
                return true;
            } catch
            {
                return false;
            }
            
        }

        public bool ScopeByEncloseOwnerId(string value)
        {
            try
            {
                Filter &= new BsonDocument("enclose_owner_id", Int64.Parse(value));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ScopeByTroubleFlag(string value)
        {
            bool boolValue = value == "1" ? true : false;
            Filter &= new BsonDocument("trouble_flag", boolValue);
            return true;
        }

        public bool ScopeByFinishedFlag(string value)
        {
            bool boolValue = value == "1" ? false : true;
            Filter &= new BsonDocument("finished", boolValue);
            return true;
        }
    }
}
