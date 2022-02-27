using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Models
{
    public class EncloseAndCNStatus
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public bool TroubleFlag { get; set; }

        public bool FinishedFlag { get; set; }

        public List<CainiaoStatusEvent> Statuses { get; set; }
    }
}
