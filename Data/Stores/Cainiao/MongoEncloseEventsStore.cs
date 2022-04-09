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

        public MongoEncloseEventsStore(IMongoDatabase mongoDB,
            ILogger<MongoEncloseEventsStore> logger)
        {
            _encloseEvents = mongoDB.GetCollection<EncloseEvent>(CollectionName);
            _logger = logger;
            AddScope("SearchEncloseId", ScopeByEncloseId);
            AddScope("SearchEncloseOwnerId", ScopeByEncloseOwnerId);
         }


        public async Task<EncloseEvent> CreateAsync(EncloseEvent encloseEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<EncloseEvent> Delete(int encloseId, int encloseOwnerID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EncloseEvent>> GetAsync()
        {
            var query = (from EncloseEvents in _encloseEvents.AsQueryable<EncloseEvent>()
                         select EncloseEvents);
            query = (IMongoQueryable<EncloseEvent>)ExtendQueryByFilter(query);

            var result = from EncloseEvents in query select EncloseEvents;
            return await result.ToListAsync();
        }

        public async Task<EncloseEvent ?> GetAsync(int encloseId, int encloseOwnerID)
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

        public IQueryable<EncloseEvent> ScopeByEncloseId(IQueryable<EncloseEvent> query, string value)
        {
            query = query.Where(enclose => enclose.EncloseId.Equals(value));
            return query;
        }

        public IQueryable<EncloseEvent> ScopeByEncloseOwnerId(IQueryable<EncloseEvent> query, string value)
        {
            query = query.Where(enclose => enclose.EncloseOwnerId.Equals(value));
            return query;
        }
    }
}
