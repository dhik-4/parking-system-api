namespace ParkingSystemAPI.CustomModels.Fare
{
    public class FareScheme
    {
        public decimal Fare { get; set; }
        public decimal UnitHour { get; set; }
        public int MaxScheme { get; set; } //0 for unlimited
    }
}
