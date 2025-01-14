using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StatutBicyclette
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<Bicyclette> Bicyclettes { get; set; } = new List<Bicyclette>();
}
