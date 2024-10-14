using Microsoft.EntityFrameworkCore;
using Nithin2003.Models;

namespace Nithin2003.Database
{
    public class ApplicationData:DbContext
    {
        public ApplicationData(DbContextOptions<ApplicationData>Options):base(Options)
        {
        }
        public DbSet<MyUser> Users { get; set; }
        public DbSet<MyTransferMoney> TransactionHistory { get; set; }

        public DbSet<MyMoneyRequest> mymoneyrequestt { get; set; }
        

    }
}
