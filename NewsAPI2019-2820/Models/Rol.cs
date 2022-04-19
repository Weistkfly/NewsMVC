using System;
using System.Collections.Generic;

#nullable disable

namespace NewsAPI2019_2820.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolUsers = new HashSet<RolUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolUser> RolUsers { get; set; }
    }
}
