using System.ComponentModel.DataAnnotations;

namespace BankAccount.Models
{
    public class UserLog
    {   
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        
        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}