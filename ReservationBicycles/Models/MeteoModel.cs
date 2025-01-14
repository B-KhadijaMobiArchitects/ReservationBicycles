using Microsoft.Identity.Client;

namespace ReservationBicycles.Models
{
    public class MeteoModel
    {
         public string meteo { get; set; }
         public string description { get; set; }
         public string ville { get; set; }
    }
    public class WeatherResponse
    {
        public CoordData Coord { get; set; }
        public WeatherData[] Weather { get; set; }
        public MainData Main { get; set; }
        public WindData Wind { get; set; }
        public CloudsData Clouds { get; set; }
        public SysData Sys { get; set; }
        public string Name { get; set; }
        public int Cod { get; set; }
    }

    public class CoordData
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class WeatherData
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class MainData
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
    }

    public class WindData
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
    }

    public class CloudsData
    {
        public int All { get; set; }
    }

    public class SysData
    {
        public int Type { get; set; }
        public int Id { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
