using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IFilmRepository Film { get; }
        public IGenereRepository Genere { get; }
        public ISalaRepository Sala { get; }
        public IBigliettoRepository Biglietto { get; }
        public IValutazioneRepository Valutazione { get; }
        public ISpettacoloRepository Spettacolo { get; }
        void Save();
    }
}
