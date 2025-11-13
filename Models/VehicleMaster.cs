using System;
using System.Collections.Generic;

namespace ParkingSystemAPI.Models;

public partial class VehicleMaster
{
    public byte VehicleMasterId { get; set; }

    public string VehicleName { get; set; } = null!;

    public string? Code { get; set; }
}
