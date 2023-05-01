using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models.ViewModel
{
    public class MieiFilmVM
    {
        public IEnumerable<Film> Films { get; set; }
        public IEnumerable<Valutazione> Valutazioni { get; set; }
        public static bool HasValutazione(string id, IEnumerable<Valutazione> valutazioni)
        {
            return valutazioni.Count() > 0 && valutazioni.Where(v => v.FkFilm == id).First() != null;
        }
    }
}
