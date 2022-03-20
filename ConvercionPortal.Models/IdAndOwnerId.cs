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
        {   string[] arr = idAndOwnerId.Split(new char[] { Separator });
            Id = int.Parse(arr[0]);
            OwnerId = int.Parse(arr[1]);
        }

        public int Id { get; }
        public int OwnerId { get; }
    }
}
