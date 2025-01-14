using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class ModePaiement
{
    public int Id { get; set; }

    public string Libelle { get; set; } = null!;

    public virtual ICollection<LocationBicyclette> LocationBicyclettes { get; set; } = new List<LocationBicyclette>();
}
