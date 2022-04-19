﻿using System;
using System.Collections.Generic;

#nullable disable

namespace NewsAPI2019_2820.Models
{
    public partial class Author
    {
        public Author()
        {
            News = new HashSet<News>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<News> News { get; set; }
    }
}
