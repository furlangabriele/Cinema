using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models;

public partial class Biglietto
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    [ForeignKey(nameof(ApplicationUserId))]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public int SpettacoloId { get; set; }
    [ForeignKey(nameof(SpettacoloId))]
    [ValidateNever]
    public Spettacolo Spettacolo { get; set; } = null!;

    public int Fila { get; set; }

    public int Posto { get; set; }
    public bool Pagato { get; set; } = false;
}
