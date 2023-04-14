using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class SalaRepository : Repository<Sala>, ISalaRepository
    {
        private AppDbContext _db;
        public SalaRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Sala obj)
        {
            _db.Update(obj);
        }
    }
}