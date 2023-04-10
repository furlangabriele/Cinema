﻿using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Repository.IRepository
{
    public interface ISpettacolo : IRepository<Spettacolo>
    {
        void Update(Spettacolo spettacolo);
    }
}
