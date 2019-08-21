using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using BankAccount.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccount
{
    public class UserReg
    {
        [Key]
        [Column("id")]
        public int UserId {get; set;}

        [Required]
        [Column("f_name")]
        [MinLength(2,ErrorMessage="First name must be at least 2 characters.")]
        public string FirstName {get; set;}

        [Required]
        [Column("l_name")]
        [MinLength(2,ErrorMessage="Last name must be at least 2 characters.")]
        public string LastName {get; set;}

        [Required]
        [Column("email")]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Column("pw")]
        [MinLength(6,ErrorMessage="Password must be at least 2 characters.")]
        public string Password {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt{get; set;} = DateTime.Now;
        
        [Column("updated_at")]
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        [NotMapped]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get; set;}

        public List<Transactions> TransMade {get; set;}

        [Column("balance")]
        public decimal Balance {get; set;} = 0;

    }

    public class Transactions
    {
        [Key]
        [Column("trans_id")]
        public int TransId {get; set;}

        public UserReg Owner {get; set;}

        [Column("user_id")]
        public int UserId {get; set;}

        [Required]
        [Column("amount")]
        public decimal Amount {get; set;}

        [Column("created_at")]
        public DateTime CreatedAt{get; set;} = DateTime.Now;
    }
}