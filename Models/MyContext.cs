using Microsoft.EntityFrameworkCore;

namespace BankAccount.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<UserReg> Users {get; set;}
        public DbSet<Transactions> Transactions {get; set;}
    }
}