using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVC2019_2820.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Rol")]
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
