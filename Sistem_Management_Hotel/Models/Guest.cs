using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Guest
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public string? Telepon { get; set; }

    public string? Email { get; set; }

    public string? NoIdentitas { get; set; }

    public string? Alamat { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
}
