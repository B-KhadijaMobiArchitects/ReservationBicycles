using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ModeleBicyclette
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public double? PrixLocation { get; set; }

    public string? Photo { get; set; }

    public virtual ICollection<Bicyclette> Bicyclettes { get; set; } = new List<Bicyclette>();
}
