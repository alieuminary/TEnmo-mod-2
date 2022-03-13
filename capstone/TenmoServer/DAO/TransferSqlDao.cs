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
                                     "JOIN transfer_type TT on T.transfer_type_id = TT.transfer_type_id " +
                                     "JOIN transfer_status TS on T.transfer_status_id = TS.transfer_status_id " +
                                     "WHERE B.user_id = @authUserId OR A.user_id = @authUserId;";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@authUserId", authUserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = GetTransferFromReader(reader);
                        transfer = GetUserIdsAndUsernames(transfer);
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

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT * FROM transfer T " +
                                     "JOIN account A on T.account_from = A.account_id " +
                                     "JOIN account B on T.account_to = B.account_id " +
                                     "JOIN transfer_type TT on T.transfer_type_id = TT.transfer_type_id " +
                                     "JOIN transfer_status TS on T.transfer_status_id = TS.transfer_status_id " +
                                     "WHERE T.transfer_id = @transferId;";

                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@transferId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        transfer = GetTransferFromReader(reader);
                        
                    }

                    

                }

                transfer = GetUserIdsAndUsernames(transfer);

            }
            catch (SqlException)
            {
                throw;
            }

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

            Transfer transfer = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferTypeDesc = Convert.ToString(reader["transfer_type_desc"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                TransferStatusDesc = Convert.ToString(reader["transfer_status_desc"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
            };

            return transfer;

        }

        private Transfer GetUserIdsAndUsernames(Transfer transfer)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT user_id FROM account WHERE account_id = @toUserAccountId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@toUserAccountId", transfer.AccountTo);
                    try
                    {
                        transfer.ToUserId = (int)cmd.ExecuteScalar();
                    }
                    catch(SqlException)
                    {
                        throw;
                    }
                }



                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT user_id FROM account WHERE account_id = @fromUserAccountId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@fromUserAccountId", transfer.AccountFrom);

                    transfer.FromUserId = (int)cmd.ExecuteScalar();
                }



                // ===== Assign to toUsername ==== //
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT username FROM tenmo_user WHERE user_id = @toUserId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@toUserId", transfer.ToUserId);

                    transfer.ToUsername = cmd.ExecuteScalar().ToString();
                }


                // ===== Assign to fromUsername ==== //
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT username FROM tenmo_user WHERE user_id = @fromUserId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@fromUserId", transfer.FromUserId);

                    transfer.FromUsername = cmd.ExecuteScalar().ToString();
                }
            }
            catch(NullReferenceException ex)
            {
                throw;
            }
            return transfer;
        }

        public List<Transfer> GetPendingTransfers(string authUserId)
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
                                     //"JOIN account B on T.account_to = B.account_id " +
                                     "JOIN transfer_type TT on T.transfer_type_id = TT.transfer_type_id " +
                                     "JOIN transfer_status TS on T.transfer_status_id = TS.transfer_status_id " +
                                     "WHERE (A.user_id = @authUserId) AND TS.transfer_status_id = 1;";//B.user_id = @authUserId OR 
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@authUserId", authUserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Transfer transfer = GetTransferFromReader(reader);
                        transfer = GetUserIdsAndUsernames(transfer);
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

        public bool UpdateStatusId(Transfer transfer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "UPDATE transfer " +
                                     "SET transfer_status_id = @transferStatusId " + 
                                     "WHERE transfer_id = @transfer_id";

                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@transferStatusId", transfer.TransferStatusId);
                    cmd.Parameters.AddWithValue("@transfer_id", transfer.TransferId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true; // change was successful
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

    }
}
