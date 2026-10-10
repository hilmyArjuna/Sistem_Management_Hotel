using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Room
{
    public int Id { get; set; }

    public int RoomTypeId { get; set; }

    public string Nomor { get; set; } = null!;

    public int? Lantai { get; set; }

    public string? Fasilitas { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<HousekeepingLog> HousekeepingLogs { get; set; } = new List<HousekeepingLog>();

    public virtual ICollection<RoomBookingDetail> RoomBookingDetails { get; set; } = new List<RoomBookingDetail>();

    public virtual RoomType RoomType { get; set; } = null!;
}
