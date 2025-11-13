using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParkingSystemAPI.Models;

public partial class TransactionParking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TransactionId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public string RefNumber { get; set; } = null!;

    public DateTime TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }

    public decimal Tariff { get; set; }

    public byte UnitMasterId { get; set; }

    public byte VehicleMasterId { get; set; }

    public decimal? TotalPay { get; set; }
}
