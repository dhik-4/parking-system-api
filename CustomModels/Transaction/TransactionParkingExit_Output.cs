namespace ParkingSystemAPI.CustomModels.Transaction
{
    public partial class TransactionParkingExit_Output
    {
        public string PlateNumber { get; set; } = null!;

        public string RefNumber { get; set; }
        public string Vehicle { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Duration {  get; set; }
        public decimal TotalPay { get; set; }
    }
}