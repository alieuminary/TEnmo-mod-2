using RestSharp;
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















        //public List<Transfer> ListTransfers()
        //rest request to GET("transfer")
        //client.Gets<List<Transfer>>(request)



    }
}
