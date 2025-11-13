namespace ParkingSystemAPI.CustomModels.Transaction
{
    public partial class TransactionParkingEnter_Input
    {
        public string PlateNumber { get; set; } = null!;

        public string VehicleCode { get; set; }

    }
}