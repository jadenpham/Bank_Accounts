using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using BankAccount.Models;

namespace BankAccount
{
    public class UserTransComb
    {
        public UserReg NewUser {get; set;}
        public Transactions NewTrans{get; set;}
    }
}