using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Account
    {
        public int account_id { get; set; }
        public int user_id { get; set; }
        public decimal balance { get; set; }
    }
}
