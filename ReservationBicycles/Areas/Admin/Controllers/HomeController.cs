using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReservationBicycles.Models;

namespace ReservationBicycles.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index()
        {
            //var City = "Casablanca";
            //string apiKey = "4ff19048131f07394e18b88df5fd1c71"; // Remplacez par votre clé API OpenWeatherMap
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q=Casablanca&appid=4ff19048131f07394e18b88df5fd1c71";
            var result = new MeteoModel();
            try
            {
                // Appel de l'API OpenWeatherMap
                var response = await _httpClient.GetAsync(apiUrl);

                // Vérifier si la réponse est réussie
                if (!response.IsSuccessStatusCode)
                {
                    return View(result);
                }

                // Lire le contenu de la réponse JSON
                var jsonResponse = await response.Content.ReadAsStringAsync();

                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonResponse);


                if (weatherResponse == null || weatherResponse.Main == null)
                {
                    return View(result);

                }


                result.ville = "Casablanca";
                result.meteo = $"{((int)weatherResponse.Main.Temp-273)}°C";
                result.description = weatherResponse.Weather?[0]?.Description;


                return View(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

    }
}
