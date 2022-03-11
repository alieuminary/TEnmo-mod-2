using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TenmoServer.Security;
using TenmoServer.Security.Models;
using TenmoServer.Models;


namespace TenmoServer.DAO
{
    public class TransferSqlDao : ITransferDao
    {

        private readonly string connectionString;

        public TransferSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public List<Transfer> GetTransfers(string authUserId)
        {

            //string authUserId = null//User.FindFirst("sub")?.Value; // User.Identity.Name;

            List<Transfer> transfers = new List<Transfer>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT * FROM transfer T " +
                                     "JOIN account A on T.account_from = A.account_id " +
                                     "JOIN account B on T.account_to = B.account_id " +
                                     "WHERE B.user_id = @authUserId OR A.user_id = @authUserId;";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@authUserId", authUserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = GetTransferFromReader(reader);
                        transfers.Add(transfer);
                    }


                }
            }
            catch (SqlException)
            {
                throw;
            }

            return transfers;

        }

        public Transfer GetTransferDetails(int id) 
        {
            Transfer transfer = new Transfer();
            return transfer;
        }

        public int AddTransfer(int transferTypeId, int transferStatusId, int accountFrom, int accountTo, decimal amount)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "INSERT INTO transfer ( transfer_type_id, transfer_status_id, account_from, account_to, amount) " +//transfer_type_id, transfer_status_id,
                                    "OUTPUT INSERTED.transfer_id " + 
                                     "VALUES (@transferTypeId, @transferStatusId,  @accountFrom, @accountTo, @amount)";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@transferTypeId", transferTypeId);
                    cmd.Parameters.AddWithValue("@transferStatusId", transferStatusId);
                    cmd.Parameters.AddWithValue("@accountFrom", accountFrom);
                    cmd.Parameters.AddWithValue("@accountTo", accountTo);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    int newTransferId = 0;
                    newTransferId = (int)cmd.ExecuteScalar();

                    return newTransferId;
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }


        private Transfer GetTransferFromReader(SqlDataReader reader)
        {

            //Transfer transfer = new Transfer();

            //transfer.TransferId = Convert.ToInt32(reader["transfer_id"]);
            //transfer.TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]);
            //transfer.TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]);
            //transfer.AccountFrom = Convert.ToInt32(reader["account_from"]);
            //transfer.AccountTo = Convert.ToInt32(reader["account_to"]);
            //transfer.Amount = Convert.ToDecimal(reader["amount"]);

            Transfer transfer = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
            };

            return transfer;

        }
    }
}
