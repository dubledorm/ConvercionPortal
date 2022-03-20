using MongoDB.Bson;

namespace ConvercionPortal.Models
{
    public class CnEncloseStatus
    {
        public ObjectId Id { get; set; }
        public int EncloseId { get; set; }

        public int EncloseOwnerId { get; set; }

        public bool TroubleFlag { get; set; }

        public bool FinishedFlag { get; set; }

        public List<CnStatusEvent> StatusHistory { get; set; }

        public string StringId()
        {
            return $"{EncloseId}-{EncloseOwnerId}";
        }
    }
}
