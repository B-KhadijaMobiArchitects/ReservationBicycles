using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class StatutReservation
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Libelle { get; set; } = null!;

    public virtual ICollection<LocationBicyclette> LocationBicyclettes { get; set; } = new List<LocationBicyclette>();
}
