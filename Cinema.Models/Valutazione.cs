using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public partial class Valutazione
{
    public string ApplicationUserId { get; set; } = null!;
    [ForeignKey(nameof(ApplicationUserId))]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string FkFilm { get; set; } = null!;
    public int Valutazione1 { get; set; }

    public string? Commento { get; set; }

    public virtual Film FkFilmNavigation { get; set; } = null!;
}
