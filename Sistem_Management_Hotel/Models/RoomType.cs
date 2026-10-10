using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class RoomType
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public string? Deskripsi { get; set; }

    public int? Kapasitas { get; set; }

    public decimal HargaDasar { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
