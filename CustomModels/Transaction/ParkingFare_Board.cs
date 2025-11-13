namespace ParkingSystemAPI.CustomModels.Transaction
{
    public class ParkingFare_Board
    {
        //public int RowNumber {  get; set; }
        public string VehicleName { get; set; }
        public string Code { get; set; }
        public decimal Fare {  get; set; }
        public string Unit {  get; set; }
        
    }
}
