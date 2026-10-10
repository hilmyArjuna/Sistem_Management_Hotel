using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Facility
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
