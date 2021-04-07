


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using Utilities;
using Utilities.Models;
using static Utilities.Models.Transaction;

namespace WebApiServer.Controllers
{
        

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        public RepositoryContext DbContext;
        public User user;
        public Transaction transaction;
        const string response = "Transaction successfull!\nYour new remaining balance is : ";
       

        public TransactionController(RepositoryContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }

        [NonAction]
        protected IActionResult CommonOperation(long user_id, long reciever_id, TransactionType act_type, decimal amount)
        {
            
            decimal balance = 0;
            //check if user is existing
            bool account = IsAccountExist(user_id);
            if (!account) { return NotFound("No ID found!"); }

            //get the last transaction of the user
            transaction = GetLastTransaction(user_id);

            switch (act_type)
            {
                case TransactionType.Deposit:
                    balance = DepositAmount(amount);
                    break;
                case TransactionType.Withdraw:
                    //check if sufficient balance to withdraw
                    if (amount > transaction.CurrentBalance) return BadRequest("Sorry you do not have enough balance!");
                    balance = WithdrawAmount(amount);
                    break;
                case TransactionType.Transfer:
                    //check if sufficient balance to withdraw
                    if (amount > transaction.CurrentBalance) return BadRequest("Sorry you do not have enough balance!");
                    //check if valid receipient
                    if (!IsAccountExist(reciever_id)) return NotFound("Receipient ID is not found, please check and try again!");
                    balance = TransferAmount(amount, reciever_id);
                    break;
                default:
                    //nothing
                    break;
            }
            SaveChanges();
            return Ok(response + balance);
        }

        #region Actions
        //api/Transaction/1/checkbal
        [HttpGet("{id}/checkbal")]
        public IActionResult GetBalance(long id)
        {
            //get current balance from the latest transaction
            decimal bal = GetLastTransaction(id).CurrentBalance;

            return Ok("Your remaining balance is " + bal);
        }

        //api/Transaction/1/deposit?amount=1200
        [HttpPut("{id}/deposit")]
        public IActionResult Deposit(long id, decimal amount)
        {
            return CommonOperation(id, id, TransactionType.Deposit, amount);
        }

        //api/Transaction/1/withdraw?amount=1000
        [HttpPut("{id}/withdraw")]
        public IActionResult Withdraw(long id, decimal amount)
        {
            return CommonOperation(id, id, TransactionType.Withdraw, amount);
        }
        //api/Transaction/1/deposit?ref_id2&amount=1000
        [HttpPut("{id}/transfer")]
        public IActionResult Transfer(long id, long ref_id, decimal amount)
        {
            return CommonOperation(id, ref_id, TransactionType.Transfer, amount);
        }
        #endregion

        #region Helpers
        private decimal DepositAmount(decimal amount)
        {
            //get current balance and add deposited amount
            decimal newbalance = transaction == null ? amount : amount + transaction.CurrentBalance;

            DbContext.transactions.Add(new Transaction
            {
                Amount = amount,
                CurrentBalance = newbalance,
                Type = 1,
                Date = DateTime.Now,
                UserId = user.Id,
                Account = user.Id,
                PrevBalance = transaction == null ? 0 : transaction.CurrentBalance,
            });

            return newbalance;
        }

        private decimal WithdrawAmount(decimal amount)
        {
            //get current balance and less the withdrawal amount
            decimal new_balance = transaction.CurrentBalance - amount;
            DbContext.transactions.Add(new Transaction
            {
                Amount = amount,
                CurrentBalance = new_balance,
                Type = 2,
                Date = DateTime.Now,
                UserId = user.Id,
                Account = user.Id,
                PrevBalance = transaction.CurrentBalance,
            });

            return new_balance;
        }

        private decimal TransferAmount(decimal amount, long reciever_id)
        {
            //get current balance of sender and less the transferred amount 
            decimal debit_balance = transaction.CurrentBalance - amount;

            //transaction for sender
            DbContext.transactions.Add(new Transaction
            {
                Amount = amount,
                CurrentBalance = debit_balance,
                Type = 3,
                Date = DateTime.Now,
                UserId = transaction.UserId,
                Account = reciever_id,
                PrevBalance = transaction.CurrentBalance,
            });


            Transaction receiver = GetLastTransaction(reciever_id);
            //get current balance of receiptient and add the transferred amount 
            decimal credit_balance = receiver.CurrentBalance + amount;

            DbContext.transactions.Add(new Transaction
            {
                Amount = amount,
                CurrentBalance = credit_balance,
                Type = 3,
                Date = DateTime.Now,
                UserId = user.Id,
                Account = transaction.UserId,
                PrevBalance = receiver.CurrentBalance,
            });

            return debit_balance;
        }
       
        //get user with id
        private bool IsAccountExist(long id)
        {
            user = DbContext.users.Find(id);
            if (user == null) { return false; }
            return true;
        }

        //get the latest transaction
        private Transaction GetLastTransaction(long id)
        {
            Transaction transaction = DbContext.transactions
                .Where(a => a.UserId == id)
                .OrderByDescending(a => a.Date)
                .FirstOrDefault();

            return transaction;
        }


        void SaveChanges()
        {
            //save to db
            DbContext.SaveChanges();
        }
        #endregion region
    }
}
