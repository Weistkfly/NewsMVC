using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsMVC2019_2820.Models.ViewModels
{
    public class NewsVM
    {
        public News News { get; set; }
        public IEnumerable<SelectListItem> AuthorDropDown { get; set; }
        public IEnumerable<SelectListItem> CategoryDropDown { get; set; }
        public IEnumerable<SelectListItem> CountryDropDown { get; set; }
    }
}
