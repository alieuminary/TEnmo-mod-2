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

        // method that gets transfer details based off of transfer_id - USE CASE 6
        [HttpGet("{id}")]

        public Transfer GetTransferDetails(int id)
        {
            Transfer result = transferDao.GetTransferDetails(id);

            return result;

        }

        [HttpGet("pending")]
        public ActionResult<List<Transfer>> ListPendingTransfers()
        {
            List<Transfer> result = new List<Transfer>();

            string authUserId = User.FindFirst("sub")?.Value;
            if (authUserId == null)
            {
                return NotFound();
            }
            else
            {
                result = transferDao.GetPendingTransfers(authUserId);
            }

            return Ok(result);
        }




        [HttpPost()]
        public ActionResult AddTransfer(Transfer transferToAdd) //<Transfer>
        {
            int newTransferId = transferDao.AddTransfer(transferToAdd.TransferTypeId, transferToAdd.TransferStatusId, transferToAdd.AccountFrom, transferToAdd.AccountTo, transferToAdd.Amount);

            if (newTransferId > 0)
            {
                return Ok(); //Created();
            }
            return NotFound();
        }



        [HttpPut("{id}")]

        public ActionResult<Transfer> UpdateStatusId(int id, Transfer transfer)
        {

            bool result = transferDao.UpdateStatusId(transfer);

            if (result == true)
            {
                Transfer updatedTransfer = transferDao.GetTransferDetails(id);
                return Ok(updatedTransfer);
            }
            return NotFound();
        }

    }
}
