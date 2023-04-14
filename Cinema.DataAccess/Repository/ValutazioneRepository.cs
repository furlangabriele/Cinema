using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class ValutazioneRepository : Repository<Valutazione>, IValutazioneRepository
    {
        private AppDbContext _db;
        public ValutazioneRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Valutazione obj)
        {
            _db.Update(obj);
        }
    }
}