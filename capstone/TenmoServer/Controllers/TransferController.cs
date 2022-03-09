using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly ITransferDao transferDao;

        public TransferController(ITransferDao _transferDao, IUserDao _userDao)
        {
            transferDao = _transferDao;
            userDao = _userDao;
        }

        [HttpGet()]
        public ActionResult<List<Transfer>> ListTransfers()
        {
            List<Transfer> result = new List<Transfer>();

            string authUserId = User.FindFirst("sub")?.Value;
            if(authUserId == null)
            {
                return NotFound();
            }
            else
            {
                result = transferDao.GetTransfers(authUserId);
            }
        
            return Ok(result);
            
        }

    }
}
