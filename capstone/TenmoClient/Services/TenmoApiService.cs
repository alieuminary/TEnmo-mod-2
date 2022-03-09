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

        //public List<Transfer> ListTransfers()
        //rest request to GET("transfer")
        //client.Gets<List<Transfer>>(request)



    }
}
