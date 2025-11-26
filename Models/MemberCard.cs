using System;
using System.Collections.Generic;

namespace ParkingSystemAPI.Models;

public partial class MemberCard
{
    public int CardId { get; set; }

    public string CardNumber { get; set; } = null!;

    public int VehicleMasterId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public DateOnly? ExpireDate { get; set; }

    public byte? Status { get; set; }
}
