using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Cinema.Models;

public partial class Spettacolo
{
    public int Id { get; set; }
    public int FkSala { get; set; }

    public string FkFilm { get; set; } = null!;

    public DateOnly Data { get; set; }

    public TimeOnly Orario { get; set; }
    [ValidateNever]
    [JsonIgnore]
    public virtual ICollection<Biglietto> Bigliettos { get; } = new List<Biglietto>();
    [ValidateNever]
    [JsonIgnore]
    public virtual Film FkFilmNavigation { get; set; } = null!;
    [ValidateNever]
    [JsonIgnore]
    public virtual Sala FkSalaNavigation { get; set; } = null!;
}
