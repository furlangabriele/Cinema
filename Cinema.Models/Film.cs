using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cinema.Models;

public partial class Film
{
    [MaxLength(20)]
    public string Titolo { get; set; } = null!;

    public string Genere { get; set; } = null!;
    [MaxLength(500)]
    public string Descrizione { get; set; } = null!;

    public int Durata { get; set; }

    public short? AnnoProd { get; set; }

    public string? UrlImmPub { get; set; }
    [ValidateNever]
    [JsonIgnore]
    public virtual Genere GenereNavigation { get; set; } = null!;
    [ValidateNever]
    [JsonIgnore]
    public virtual ICollection<Spettacolo> Spettacolos { get; } = new List<Spettacolo>();
    [ValidateNever]
    [JsonIgnore]
    public virtual ICollection<Valutazione> Valutaziones { get; } = new List<Valutazione>();
}
