using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Models
{
    public class CnStatusEvent
    {
        public int PPStatus { get; set; }

        public int PPStatusCode { get; set; }

        public WarehouseTypeEvent PPWarehouseType { get; set; }

        public DateTime DateOfCreate { get; set; }

        public int CnStatusCode { get; set; }

    }
}
