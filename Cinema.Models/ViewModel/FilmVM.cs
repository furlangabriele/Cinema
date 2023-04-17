using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModel
{
    public class FilmVM
    {
        public Film Film { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> GenereList { get; set; }

    }
}
