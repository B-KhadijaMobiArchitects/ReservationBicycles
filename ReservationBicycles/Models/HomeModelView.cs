using DAL.Entities;

namespace ReservationBicycles.Models
{
    public class HomeModelView
    {
        public List<Bicyclette> ListBicyclette { get; set; }
        public List<ModeleBicyclette> modeleBicyclettes { get; set; }
        public List<Bicyclette> bicyclettesImportnte { get; set; }

    }
}
