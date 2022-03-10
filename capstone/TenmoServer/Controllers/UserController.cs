using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao _userDao)
        {
            userDao = _userDao;
        }

        [HttpGet()]
        public List<User> ListUsers()
        {
            return userDao.GetUsers();
        }
    }
}
