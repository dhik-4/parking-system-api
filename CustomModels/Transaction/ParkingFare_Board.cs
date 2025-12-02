using ParkingSystemAPI.CustomModels.Fare;
using System.Text.Json.Serialization;

namespace ParkingSystemAPI.CustomModels.Transaction
{
    public class ParkingFare_Board
    {
        public int ID { get; set; }
        public string? VehicleName { get; set; }
        public string? Code { get; set; }
        public decimal Fare {  get; set; }
        //public string Unit { get; set; }

        [JsonIgnore]
        public string? FareSchemeJson { get; set; }
        public byte? IsActive { get; set; }

        public List<FareScheme>? FareSchemes { get; set; } = new List<FareScheme>();
    }
}
