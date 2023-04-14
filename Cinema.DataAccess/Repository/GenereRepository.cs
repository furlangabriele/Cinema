using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class GenereRepository : Repository<Genere>, IGenereRepository
    {
        private AppDbContext _db;
        public GenereRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Genere obj)
        {
            _db.Update(obj);
        }
    }
}