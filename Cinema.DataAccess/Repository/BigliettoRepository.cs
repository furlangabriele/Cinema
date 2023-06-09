﻿using Cinema.DataAccess.Repository.IRepository;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository
{
    public class BigliettoRepository : Repository<Biglietto>, IBigliettoRepository
    {
        private AppDbContext _db;
        public BigliettoRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Biglietto obj)
        {
            _db.Update(obj);
        }
    }
}
