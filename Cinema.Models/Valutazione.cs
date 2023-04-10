using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Valutazione
{
    public string FkFilm { get; set; } = null!;

    public int FkUtente { get; set; }

    public int Valutazione1 { get; set; }

    public string? Commento { get; set; }

    public virtual Film FkFilmNavigation { get; set; } = null!;

    public virtual Utente FkUtenteNavigation { get; set; } = null!;
}
