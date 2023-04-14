using Cinema.DataAccess;
using Cinema.DataAccess.Repository;
using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Film = new FilmRepository(_db);
            Genere = new GenereRepository(_db);
            Sala = new SalaRepository(_db);
            Biglietto = new BigliettoRepository(_db);
            Valutazione = new ValutazioneRepository(_db);
            Spettacolo = new SpettacoloRepository(_db);
        }
        public IFilmRepository Film { get; private set; }
        public IGenereRepository Genere { get; private set; }
        public ISalaRepository Sala { get; private set; }
        public IBigliettoRepository Biglietto { get; private set; }
        public IValutazioneRepository Valutazione { get; private set; }
        public ISpettacoloRepository Spettacolo { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
