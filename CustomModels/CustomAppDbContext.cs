using Microsoft.EntityFrameworkCore;
using ParkingSystemAPI.CustomModels.Transaction;

namespace ParkingSystemAPI.CustomModels
{
    public partial class CustomAppDbContext : DbContext
    {
        public CustomAppDbContext()
        {
        }

        public CustomAppDbContext(DbContextOptions<CustomAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ParkingFare_Board> ParkingFare_Boards { get; set; }
        public virtual DbSet<TransactionParking_Trx> TransactionParking_Trxs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingFare_Board>(entity =>
               entity.HasNoKey());
            modelBuilder.Entity<TransactionParking_Trx>(entity =>
              entity.HasNoKey());
        }
    }
}
