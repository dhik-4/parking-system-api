namespace ParkingSystemAPI.CustomModels.Transaction
{
    public class TransactionParking_Trx
    {
        public string PlateNumber { get; set; } = null!;

        public string RefNumber { get; set; } = null!;

        public DateTime TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public string VehicleName { get; set; }

        public decimal? TotalPay { get; set; }
    }
}
