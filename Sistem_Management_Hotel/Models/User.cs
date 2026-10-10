using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Nama { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Telepon { get; set; }

    public bool? Aktif { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<HousekeepingLog> HousekeepingLogs { get; set; } = new List<HousekeepingLog>();

    public virtual ICollection<PurchaseRequest> PurchaseRequestApprovedByNavigations { get; set; } = new List<PurchaseRequest>();

    public virtual ICollection<PurchaseRequest> PurchaseRequestCreatedByNavigations { get; set; } = new List<PurchaseRequest>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<RoomBooking> RoomBookings { get; set; } = new List<RoomBooking>();
}
