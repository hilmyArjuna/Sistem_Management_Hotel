using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class RoomBookingDetail
{
    public long Id { get; set; }

    public long BookingId { get; set; }

    public int RoomId { get; set; }

    public string? Catatan { get; set; }

    public virtual RoomBooking Booking { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
