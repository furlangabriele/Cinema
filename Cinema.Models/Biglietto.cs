using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public partial class Biglietto
{
    public string ApplicationUserId { get; set; } = null!;
    [ForeignKey(nameof(ApplicationUserId))]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public int FkSala { get; set; }

    public string FkFilm { get; set; } = null!;

    public DateOnly Data { get; set; }

    public TimeOnly Orario { get; set; }

    public int Fila { get; set; }

    public int Posto { get; set; }

    public virtual Spettacolo Spettacolo { get; set; } = null!;
}
