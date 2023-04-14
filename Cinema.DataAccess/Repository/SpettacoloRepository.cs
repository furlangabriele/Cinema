using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class SpettacoloRepository : Repository<Spettacolo>, ISpettacoloRepository
    {
        private AppDbContext _db;
        public SpettacoloRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Spettacolo obj)
        {
            _db.Update(obj);
        }
    }
}
