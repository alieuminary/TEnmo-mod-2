﻿using RestSharp;
using System.Collections.Generic;
using TenmoClient.Models;

namespace TenmoClient.Services
{
    public class TenmoApiService : AuthenticatedApiService
    {
        public readonly string ApiUrl;

        public TenmoApiService(string apiUrl) : base(apiUrl) { }

        // Add methods to call api here...

        public Account GetBalance()
        {
            RestRequest request = new RestRequest("account");
            IRestResponse<Account> response = client.Get<Account>(request);

            return response.Data;
        }

        public List<User> GetUsers()
        {
            RestRequest request = new RestRequest("user");
            IRestResponse<List<User>> response = client.Get<List<User>>(request);

            return response.Data;
        }

        // create put to update balance in the account entity/table
        public Account UpdateBalance(Account accountToUpdate)
        {
            RestRequest request = new RestRequest($"account/{accountToUpdate.UserId}");
            request.AddJsonBody(accountToUpdate);
            IRestResponse<Account> response = client.Put<Account>(request);
            //check for error?
            return response.Data;
        }

        public Account GetAccount(int userId)
        {
            RestRequest request = new RestRequest($"account/{userId}");
            IRestResponse<Account> response = client.Get<Account>(request);

            return response.Data;
        }
















        //public List<Transfer> ListTransfers()
        //rest request to GET("transfer")
        //client.Gets<List<Transfer>>(request)



    }
}
