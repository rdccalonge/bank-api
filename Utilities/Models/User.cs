using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Models
{
    [Table(name: "User")]
    public class User
    {
        ///[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        [Column(TypeName = "varchar(500)")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        [Column(TypeName = "varchar(500)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [Column(TypeName = "varchar(500)")]
        public string LastName { get; set; }

        public DateTime CreateDateTime { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        
    }  
}
