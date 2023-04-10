using System;
using System.Collections.Generic;
using Cinema.Models;

namespace Cinema.Models;

public partial class Sala
{
    public int Numero { get; set; }

    public int PostiDisponibili { get; set; }

    public bool Isense { get; set; }

    public virtual ICollection<Spettacolo> Spettacolos { get; } = new List<Spettacolo>();
}
