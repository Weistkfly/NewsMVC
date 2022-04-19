using System;
using System.Collections.Generic;

#nullable disable

namespace NewsAPI2019_2820.Models
{
    public partial class RolUser
    {
        public int RolesId { get; set; }
        public int UsersId { get; set; }

        public virtual Rol Roles { get; set; }
        public virtual User Users { get; set; }
    }
}
