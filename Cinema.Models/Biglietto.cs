using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Biglietto
{
    public int FkUtente { get; set; }

    public int FkSala { get; set; }

    public string FkFilm { get; set; } = null!;

    public DateOnly Data { get; set; }

    public TimeOnly Orario { get; set; }

    public int Fila { get; set; }

    public int Posto { get; set; }

    public virtual Utente FkUtenteNavigation { get; set; } = null!;

    public virtual Spettacolo Spettacolo { get; set; } = null!;
}
