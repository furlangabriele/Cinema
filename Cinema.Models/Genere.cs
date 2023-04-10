using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Genere
{
    public string Genere1 { get; set; } = null!;

    public virtual ICollection<Film> Films { get; } = new List<Film>();
}
