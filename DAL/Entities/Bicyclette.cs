using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Bicyclette
{
    public int Id { get; set; }

    public int Modele { get; set; }

    public string? Libelle { get; set; }

    public double Prix { get; set; }

    public bool Disponibilite { get; set; }

    public DateTime DateAchat { get; set; }

    public int Etat { get; set; }

    public string? Photo { get; set; }

    public virtual StatutBicyclette EtatNavigation { get; set; } = null!;

    public virtual ICollection<LocationBicyclette> LocationBicyclettes { get; set; } = new List<LocationBicyclette>();

    public virtual ModeleBicyclette ModeleNavigation { get; set; } = null!;
}
