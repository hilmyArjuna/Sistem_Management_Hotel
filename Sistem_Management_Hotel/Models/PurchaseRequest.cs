using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class PurchaseRequest
{
    public long Id { get; set; }

    public string KodePr { get; set; } = null!;

    public int? SupplierId { get; set; }

    public DateTime Tanggal { get; set; }

    public decimal? TotalEstimasi { get; set; }

    public string? Alasan { get; set; }

    public string? Status { get; set; }

    public int? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public string? CatatanApproval { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? ApprovedByNavigation { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PrItem> PrItems { get; set; } = new List<PrItem>();

    public virtual Supplier? Supplier { get; set; }
}
