using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvercionPortal.Models
{
    public class IdAndOwnerId
    {
        private const char Separator = '-';

        public IdAndOwnerId(int id, int ownerId)
        {
            Id = id;
            OwnerId = ownerId;
        }

        public IdAndOwnerId(string idAndOwnerId)
        {
            Id = 1;  // (int)idAndOwnerId.Split(Separator)[0];
            OwnerId = 1;
        }

        public int Id { get; set; }
        public int OwnerId { get; set; }


    }
}
