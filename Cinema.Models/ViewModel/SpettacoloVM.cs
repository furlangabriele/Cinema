using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema.Models.ViewModel
{
    public class SpettacoloVM
    {
        public Spettacolo Spettacolo { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> FilmList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SalaList { get; set; }

    }
}
