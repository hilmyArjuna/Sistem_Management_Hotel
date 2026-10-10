using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class RoomBooking
{
    public long Id { get; set; }

    public string KodeBooking { get; set; } = null!;

    public int GuestId { get; set; }

    public DateTime TglCheckin { get; set; }

    public DateTime TglCheckout { get; set; }

    public int JumlahMalam { get; set; }

    public int JumlahKamar { get; set; }

    public string MetodeBayar { get; set; } = null!;

    public string? RefNumber { get; set; }

    public decimal TotalBayar { get; set; }

    public string? Sumber { get; set; }

    public string? Status { get; set; }

    public string? Keterangan { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Guest Guest { get; set; } = null!;

    public virtual ICollection<RoomBookingDetail> RoomBookingDetails { get; set; } = new List<RoomBookingDetail>();
}
