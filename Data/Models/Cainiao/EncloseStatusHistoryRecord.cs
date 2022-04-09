using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Data.Models.Cainiao
{
    public class EncloseStatusHistoryRecord
    {
        [Required]
        [BsonElement("pp_visual_state")]
        public int VisualState { get; set; }

        [BsonElement("operation_date")]
        public DateTime OperationDate { get; set; }

        [Required]
        [BsonElement("cainiao_callback")]
        public string CainiaoCallback { get; set; }

        [BsonElement("is_cainiao_callback_sended")]
        public bool IsCainiaoCallbackSended { get; set; }

        [BsonElement("is_final_status")]
        public bool IsFinalStatus { get; set; }

        [BsonElement("opCode")]
        public string OpCode { get; set; }

        public EncloseStatusHistoryRecord()
        { }

        public EncloseStatusHistoryRecord(int visualState, string cainiaoCallback, bool isFinalStatus, string opCode = null)
        {
            VisualState = visualState;
            CainiaoCallback = cainiaoCallback;
            IsCainiaoCallbackSended = string.IsNullOrWhiteSpace(cainiaoCallback);
            OperationDate = DateTime.Now;
            IsFinalStatus = isFinalStatus;
            OpCode = opCode;
        }
    }
}
