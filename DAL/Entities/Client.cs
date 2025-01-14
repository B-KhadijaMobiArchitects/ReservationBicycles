using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string NomClient { get; set; } = null!;

    public string PrenomClient { get; set; } = null!;

    public string Adresse { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? DateCreation { get; set; }

    public virtual ICollection<LocationBicyclette> LocationBicyclettes { get; set; } = new List<LocationBicyclette>();
}
