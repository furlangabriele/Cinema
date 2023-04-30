using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public partial class ApplicationUser : IdentityUser
{
    [Required]
    public string? Cognome { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    public string Sesso { get; set; } = null!;
    [NotMapped]
    public DateOnly DataNascita { get; set; }
    [Required]
    public string ComuneRes { get; set; } = null!;
    [ValidateNever]
    public virtual ICollection<Biglietto> Bigliettos { get; } = new List<Biglietto>();
    [ValidateNever]
    public virtual ICollection<Valutazione> Valutaziones { get; } = new List<Valutazione>();
}
