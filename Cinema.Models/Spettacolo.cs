using System;
using System.Collections.Generic;
using Cinema.Models;

namespace Cinema.Models;

public partial class Spettacolo
{
    public int FkSala { get; set; }

    public string FkFilm { get; set; } = null!;

    public DateOnly Data { get; set; }

    public TimeOnly Orario { get; set; }

    public virtual ICollection<Biglietto> Bigliettos { get; } = new List<Biglietto>();

    public virtual Film FkFilmNavigation { get; set; } = null!;

    public virtual Sala FkSalaNavigation { get; set; } = null!;
}
