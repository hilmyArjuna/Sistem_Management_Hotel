using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public string? Kontak { get; set; }

    public string? Telepon { get; set; }

    public string? Alamat { get; set; }

    public bool? Aktif { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<PurchaseRequest> PurchaseRequests { get; set; } = new List<PurchaseRequest>();
}
