using Microsoft.EntityFrameworkCore;
using Nithin2003.Models;

namespace Nithin2003.Database
{
    public class ApplicationData : DbContext
    {
        public ApplicationData(DbContextOptions<ApplicationData> Options) : base(Options)
        {
        }
        public DbSet<MyUser> Users { get; set; }
        public DbSet<MyTransferMoney> TransactionHistory { get; set; }
        public DbSet<MyLoanRequest> LoanRequest { get; set; }
        public DbSet<UserEmailVerification> EmailVerification { get; set; }
        public DbSet<UserHistory> LoginHistory { get; set; }
        public DbSet<Payment> PaymentRecords { get; set; }
        public DbSet<TrackAllOrders> OrderStatus { get; set; }
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<ShippingDetails> shippingDetails { get; set; }
        public DbSet<Track> trackOrder { get; set; }

    }
}
