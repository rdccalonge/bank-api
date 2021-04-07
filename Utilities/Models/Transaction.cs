using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Models
{
    [Table(name: "Transaction")]
    public class Transaction
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ReferenceId { get; set; }
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        //transaction type
        public int Type { get; set; }
        //account origin
        public long Account { get; set; }
        
        //transaction amount
        public decimal Amount { get; set; }
        public decimal PrevBalance { get; set; }
        public decimal CurrentBalance { get; set; }


        // transaction type enum
        public enum TransactionType
        {
            Deposit = 1,
            Withdraw = 2,
            Transfer = 3
        }

        // helpers
        public TransactionType GetTransactionType()
        {
            return (TransactionType)Type;
        }
    }
}
