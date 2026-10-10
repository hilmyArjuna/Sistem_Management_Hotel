using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Item
{
    public int Id { get; set; }

    public string Kode { get; set; } = null!;

    public string Nama { get; set; } = null!;

    public string? Kategori { get; set; }

    public string Satuan { get; set; } = null!;

    public decimal? Stok { get; set; }

    public decimal? StokMin { get; set; }

    public decimal? HargaEstimasi { get; set; }

    public bool? Aktif { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<PrItem> PrItems { get; set; } = new List<PrItem>();
}
