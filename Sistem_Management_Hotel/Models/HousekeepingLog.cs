using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class HousekeepingLog
{
    public long Id { get; set; }

    public int RoomId { get; set; }

    public string Status { get; set; } = null!;

    public string? Catatan { get; set; }

    public int UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
