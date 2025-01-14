using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class LocationBicyclette
{
    public int Id { get; set; }

    public int BicycletteId { get; set; }

    public int ClientId { get; set; }

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public DateTime? DateRetour { get; set; }

    public int ModePaiement { get; set; }

    public double Totale { get; set; }

    public int? StatutId { get; set; }

    public virtual Bicyclette Bicyclette { get; set; } = null!;

    public virtual Client Client { get; set; } = null!;

    public virtual ModePaiement ModePaiementNavigation { get; set; } = null!;

    public virtual StatutReservation? Statut { get; set; }
}
