using System;
using System.Collections.Generic;

namespace Sistem_Management_Hotel.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public byte? Level { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
