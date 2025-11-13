namespace ParkingSystemAPI.CustomModels.Transaction
{
    public partial class TransactionParkingEnter_Output
    {
        public string PlateNumber { get; set; } = null!;

        public string RefNumber { get; set; }
        public string Vehicle { get; set; }
        public DateTime TimeIn { get; set; }
    }
}