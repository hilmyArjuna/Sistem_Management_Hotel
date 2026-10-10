using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class PrItem
{
    public long Id { get; set; }

    public long PrId { get; set; }

    public int ItemId { get; set; }

    public decimal Qty { get; set; }

    public decimal HargaEstimasi { get; set; }

    public decimal Subtotal { get; set; }

    public string? Catatan { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual PurchaseRequest Pr { get; set; } = null!;
}
