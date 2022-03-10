using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace TenmoServer.Controllers
{
    [Route("[controller]")] // endpoint: /account
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDao accountDao;
        private readonly IUserDao userDao;

        public AccountController(IAccountDao _accountDao, IUserDao _userDao)
        {
            accountDao = _accountDao;
            userDao = _userDao;
        }

        [HttpGet()]
        public Account GetBalance()
        {
            string authUserId = User.FindFirst("sub")?.Value;
            Account account = accountDao.GetBalance(authUserId);

            return account;
        }


        [HttpGet("{id}")]

        public Account GetAccount(int id)
        {
            return accountDao.GetAccount(id);
        }



        [HttpPut("{id}")]
        public ActionResult<Account> UpdateBalance(int id, Account account)
        {
            //Account existingAccount = accountDao.GetBalance();
            bool result = accountDao.UpdateBalance(account);

            if (result == true)
            {
                Account updatedAccount = accountDao.GetBalance(Convert.ToString(id));
                return Ok(updatedAccount);
            }
            return NotFound();
        }




    }
}
