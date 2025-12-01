using System;
using System.Collections.Generic;

namespace ParkingSystemAPI.Models;

public partial class ParkingFare
{
    public int ParkingFareId { get; set; }

    public decimal Fare { get; set; }

    public byte UnitMasterId { get; set; }

    public byte VehicleMasterId { get; set; }

    public string? FareSchemeJson { get; set; }

    public byte? IsActive { get; set; }
}
