using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModel
{
    public class MieiBigliettiVM
    {
        public IEnumerable<Spettacolo> Spettacolos { get; set; }
        public IEnumerable<Biglietto> bigliettos { get; set; }
    }
}
