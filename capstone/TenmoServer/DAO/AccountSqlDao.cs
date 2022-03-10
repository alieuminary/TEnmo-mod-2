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
    public class AccountSqlDao : IAccountDao
    {
        private readonly string connectionString;

        public AccountSqlDao(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Account GetBalance(string authUserId)
        {
            Account result = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT * FROM account WHERE user_id = @authUserId";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@authUserId", authUserId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return result;

            
        }

        


        public bool UpdateBalance(Account account)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "UPDATE account " +
                                     "SET user_id = @user_id, balance = @balance, account_id = @account_id " + //,
                                     "WHERE user_id = @user_id";

                    
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@user_id", account.user_id);
                    cmd.Parameters.AddWithValue("@account_id", account.account_id);
                    cmd.Parameters.AddWithValue("@balance", account.balance);
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




        public Account GetAccount(int userId)
        {
            Account result = new Account();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string cmdText = "SELECT * FROM account WHERE user_id = @user_id";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = GetAccountFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return result;

        }





        private Account GetAccountFromReader(SqlDataReader reader)
        {
            Account account = new Account()
            {
                balance = Convert.ToDecimal(reader["balance"]),
                account_id = Convert.ToInt32(reader["account_id"]),
                user_id = Convert.ToInt32(reader["user_id"])
            };

            return account;
        }
    }
}
