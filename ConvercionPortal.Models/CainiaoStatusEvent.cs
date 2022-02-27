using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Models
{
    public class CainiaoStatusEvent
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }

        public int PPStatus { get; set; }

        public CNStatusEnum CNStatus { get; set; }

        public DateTime PPEventReceiveDateTime { get; set; }

        public DateTime CNStatusSendDateTime { get; set; }

        public int ProcessState { get; set; }

    }
}
