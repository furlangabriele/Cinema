using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        private AppDbContext _db;
        public FilmRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Film obj)
        {
            _db.Update(obj);
        }
    }
}