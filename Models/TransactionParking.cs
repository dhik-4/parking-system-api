using System;
using System.Collections.Generic;

namespace ParkingSystemAPI.Models;

public partial class TransactionParking
{
    public int TransactionId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public string RefNumber { get; set; } = null!;

    public DateTime TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public decimal Tariff { get; set; }

    public byte UnitMasterId { get; set; }

    public byte VehicleMasterId { get; set; }

    public decimal? TotalPay { get; set; }

    public string? CardNumber { get; set; }

    public byte? IsMember { get; set; }

    public int? ParkingFareId { get; set; }
}
