using System;
using System.Collections.Generic;

#nullable disable

namespace NewsAPI2019_2820.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int CountryId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Country Country { get; set; }
    }
}
