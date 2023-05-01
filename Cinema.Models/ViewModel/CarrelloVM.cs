using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModel
{
    public class CarrelloVM
    {
        public IEnumerable<Biglietto> Biglietti { get; set; }
        public IEnumerable<Spettacolo> Spettacoli { get; set; }
        public int Prezzo { get; set; }
    }
}
