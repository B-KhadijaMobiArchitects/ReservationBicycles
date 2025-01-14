namespace ReservationBicycles.Models
{
    public class ReservationModel
    {
        public int BicycletteId { get; set; }
        public string ClientType { get; set; }

        public string NomClient { get; set; } = null!;

        public string PrenomClient { get; set; } = null!;

        public string Adresse { get; set; } = null!;

        public string Telephone { get; set; } = null!;

        public string? Email { get; set; }
        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }
        public int ModePaiement { get; set; }


    }
}
