using System;
using System.Collections.Generic;

#nullable disable

namespace NewsAPI2019_2820.Models
{
    public partial class User
    {
        public User()
        {
            RolUsers = new HashSet<RolUser>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<RolUser> RolUsers { get; set; }
    }
}
