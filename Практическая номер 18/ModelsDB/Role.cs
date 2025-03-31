using System;
using System.Collections.Generic;

namespace Практическая_номер_18.ModelsDB;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
