using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;


namespace TenmoServer.DAO
{
    interface ITransferDao
    {
        List<Transfer> GetTransfers(string authUserId);

        Transfer GetTransferDetails(int id);

    }
}
