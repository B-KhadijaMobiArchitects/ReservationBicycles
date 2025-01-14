using Microsoft.AspNetCore.Mvc;

namespace ReservationBicycles.Controllers
{
    public class HelpController : Controller
    {
        [HttpGet("GetImage")]
        public IActionResult GetImage(string path)
        {
            if (System.IO.File.Exists(path))
            {
                var image = System.IO.File.OpenRead(path);
                var contentType = "image/jpeg"; // Adaptez si nécessaire (e.g., PNG, GIF)
                return File(image, contentType);
            }
            return NotFound("Image non trouvée.");
        }
    }
}
