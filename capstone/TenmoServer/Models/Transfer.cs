using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int TransferId { get; set; }

        public int TransferTypeId { get; set; }

        public string TransferTypeDesc { get; set; }

        public int TransferStatusId { get; set; }

        public string TransferStatusDesc { get; set; }

        public int AccountFrom { get; set; }

        public string FromUsername { get; set; }

        public int FromUserId { get; set; }

        public int AccountTo { get; set; }

        public string ToUsername { get; set; }

        public int ToUserId { get; set; }

        public decimal Amount { get; set; }
    }
}
