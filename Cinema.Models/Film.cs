using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Film
{
    public string Titolo { get; set; } = null!;

    public string Genere { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public int Durata { get; set; }

    public short? AnnoProd { get; set; }

    public string? UrlImmPub { get; set; }

    public virtual Genere GenereNavigation { get; set; } = null!;

    public virtual ICollection<Spettacolo> Spettacolos { get; } = new List<Spettacolo>();

    public virtual ICollection<Valutazione> Valutaziones { get; } = new List<Valutazione>();
}
