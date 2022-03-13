using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;


namespace TenmoServer.DAO
{
    public interface ITransferDao
    {
        List<Transfer> GetTransfers(string authUserId);

        Transfer GetTransferDetails(int id);

        int AddTransfer(int transferTypeId, int transferStatusId, int accountFrom, int accountTo, decimal amount);

        List<Transfer> GetPendingTransfers(string authUserId);

        bool UpdateStatusId(Transfer transfer);
    }
}
