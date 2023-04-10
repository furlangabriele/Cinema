using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Utente
{
    public int Id { get; set; }

    public string? Cognome { get; set; }

    public string? Nome { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sesso { get; set; } = null!;

    public DateOnly DataNascita { get; set; }

    public string ComuneRes { get; set; } = null!;

    public virtual ICollection<Biglietto> Bigliettos { get; } = new List<Biglietto>();

    public virtual ICollection<Valutazione> Valutaziones { get; } = new List<Valutazione>();
}
