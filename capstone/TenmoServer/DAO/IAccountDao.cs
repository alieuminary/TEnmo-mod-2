using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDao
    {
        Account GetBalance(string authUserId);

        bool UpdateBalance(Account account);

        Account GetAccount(int userId);



    }


}
