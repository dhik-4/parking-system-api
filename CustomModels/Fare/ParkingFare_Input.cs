namespace ParkingSystemAPI.CustomModels.Fare
{
    public class ParkingFare_Input
    {
        //public decimal Fare { get; set; }

        //public byte UnitMasterId { get; set; }

        public byte VehicleMasterId { get; set; }

        public List<FareScheme> FareScheme { get; set; }

        public byte IsActive { get; set; } = 0;
    }
}
