using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models.Cainiao
{
    public class EncloseEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("enclose_id")]
        public int EncloseId { get; set; }

        [BsonElement("enclose_owner_id")]
        public int EncloseOwnerId { get; set; }

        [BsonElement("trouble_flag")]
        public bool TroubleFlag { get; set; }

        [BsonElement("distributor")]
        public string Distributor { get; set; }

        [BsonElement("finished")]
        public bool IsFinished { get; set; }

        [BsonElement("status_history")]
        public IList<EncloseStatusHistoryRecord> StatusHistory { get; set; } = new List<EncloseStatusHistoryRecord>();

        public EncloseEvent() { }

        public EncloseEvent(int encloseId, int encloseOwnerId)
        {
            EncloseId = encloseId;
            EncloseOwnerId = encloseOwnerId;
        }
    }
}
