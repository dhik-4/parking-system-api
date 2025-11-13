using System;
using System.Collections.Generic;

namespace ParkingSystemAPI.Models;

public partial class UnitMaster
{
    public byte UnitMasterId { get; set; }

    public string Unit { get; set; } = null!;

    public byte? Orders { get; set; }
}
